using GeekBurger.Ui.Application.Options;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Ui.Api.ServiceBus
{
    public class UIServiceBus
    {
        private readonly ServiceBusOptions _configuration;
        private const string Topic = "UICommand";
        private List<Message> _messages;
        private Task _lastTask;
        private IServiceBusNamespace _namespace;

        public UIServiceBus(IOptions<ServiceBusOptions> configuration)
        {
            _configuration = configuration.Value;
            _messages = new List<Message>();
            _namespace = ServiceBusNamespaceExtension.GetServiceBusNamespace(_configuration);
            EnsureTopicIsCreated();
        }

        public void EnsureTopicIsCreated()
        {
            if (!_namespace.Topics.List().Any(topic => topic.Name.Equals(Topic, StringComparison.InvariantCultureIgnoreCase)))
                _namespace.Topics.Define(Topic).WithSizeInMB(1024).Create();
        }

        public void AddToMessageList<T>(T messageObject, string label)
        {
            _messages.Add(GetMessage(messageObject, label));
        }

        public Message GetMessage<T>(T entity, string label)
        {
            var entitySerialized = JsonConvert.SerializeObject(entity);
            var entityByteArray = Encoding.UTF8.GetBytes(entitySerialized);

            return new Message
            {
                Body = entityByteArray,
                MessageId = Guid.NewGuid().ToString(),
                Label = label
            };
        }

        public async void SendMessagesAsync()
        {
            if (_lastTask != null && !_lastTask.IsCompleted)
                return;

            var topicClient = new TopicClient(_configuration.ConnectionString, Topic);

            _lastTask = SendAsync(topicClient);

            await _lastTask;

            var closeTask = topicClient.CloseAsync();
            await closeTask;
            HandleException(closeTask);
        }

        public async Task SendAsync(TopicClient topicClient)
        {
            int tries = 0;
            Message message;
            while (true)
            {
                if (_messages.Count <= 0)
                    break;

                lock (_messages)
                {
                    message = _messages.FirstOrDefault();
                }

                var sendTask = topicClient.SendAsync(message);
                await sendTask;
                var success = HandleException(sendTask);

                if (!success)
                    Thread.Sleep(10000 * (tries < 60 ? tries++ : tries));
                else
                    _messages.Remove(message);
            }
        }

        public bool HandleException(Task task)
        {
            if (task.Exception == null || task.Exception.InnerExceptions.Count == 0) return true;

            task.Exception.InnerExceptions.ToList().ForEach(innerException =>
            {
                Console.WriteLine($"Error in SendAsync task: {innerException.Message}. Details:{innerException.StackTrace} ");

                if (innerException is ServiceBusCommunicationException)
                    Console.WriteLine("Connection Problem with Host. Internet Connection can be down");
            });

            return false;
        }
    }
}
