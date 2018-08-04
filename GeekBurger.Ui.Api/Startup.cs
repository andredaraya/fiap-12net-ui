using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using GeekBurger.Ui.Application.Options;
using GeekBurger.Ui.Application.ServiceBus;
using GeekBurger.Ui.Application.ServiceBus.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Reflection;

namespace GeekBurger.Ui.Api
{
    public class Startup
    {
        public IConfiguration _configuration { get; set; }

        public Startup(IConfiguration Configuration)
        {
            this._configuration = Configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "GeekBurger UI Service",
                        Version = "v1",
                        Description = "API REST para consumo do serviço UI",
                    });
            });
            services.Configure<ServiceBusOptions>(_configuration.GetSection("ServiceBus"));
            services.AddSignalR();
            services.AddSingleton<IReceiveMessagesFactory, ReceiveMessagesFactory>();
            var builder = new ContainerBuilder();
            builder.Populate(services);

            var appContainer = InitializeContainer(builder);

           

            return appContainer.Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(l=> l.SwaggerEndpoint("/swagger/v1/swagger.json", "UI"));

            app.UseSignalR(routes =>
            {
                routes.MapHub<MessageHub>("/messagehub");
            });

            app.ApplicationServices.GetService<IReceiveMessagesFactory>();
        }

        protected virtual IContainer InitializeContainer(ContainerBuilder builder, params IModule[] modules)
        {
            var crossCuttingAssembly = typeof(CrossCutting.UiModule).GetTypeInfo().Assembly;
            var options = new InitializeOptions(new[] { crossCuttingAssembly }, modules, ContainerBuildOptions.None);

            return InitializeContainer(options, builder);
        }

        public static IContainer InitializeContainer(InitializeOptions options, ContainerBuilder builder)
        {
            if (!(options.AssembliesToScan == null || options.AssembliesToScan.Length <= 0))
                builder.RegisterAssemblyModules(options.AssembliesToScan);

            if (!(options.Modules == null || options.Modules.Length <= 0))
                foreach (var module in options.Modules)
                    builder.RegisterModule(module);

            return builder.Build(options.BuildOptions);
        }
    }

    public class InitializeOptions
    {
        public Assembly[] AssembliesToScan { get; private set; }
        public IModule[] Modules { get; private set; }
        public ContainerBuildOptions BuildOptions { get; private set; }

        public InitializeOptions(Assembly[] collection, IModule[] modules, ContainerBuildOptions buildOptions)
        {
            this.AssembliesToScan = collection;
            this.Modules = modules;
            this.BuildOptions = buildOptions;
        }
    }
}
