using Flurl;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace GeekBurger.Ui.Application.Options
{
    public class UserOptions
    {
        private const string SECTION_NAME = "UserService";

        public string EndPoint { get; set; }
        public string PostUser { get; set; }
        public string PostFoodRestrictions { get; set; }

        public UserOptions(IConfiguration configuration)
        {
            var section = configuration.GetSection(SECTION_NAME);
            var endpoint = section["EndPoint"];
            var postUser = section["PostUser"];
            var postFoodRestrictions = section["PostFoodRestrictions"];

            ValidateConfiguration(endpoint, postUser, postFoodRestrictions);

            this.EndPoint = endpoint;
            this.PostUser = Url.Combine(endpoint, postUser);
            this.PostFoodRestrictions = Url.Combine(endpoint, postFoodRestrictions);
        }

        private void ValidateConfiguration(string endpoint, string postUser, string postFoodRestrictions)
        {
            if (string.IsNullOrEmpty(endpoint))
                throw new KeyNotFoundException($"Config key {SECTION_NAME}:Endpoint not set.");
            if (string.IsNullOrEmpty(postUser))
                throw new KeyNotFoundException($"Config key {SECTION_NAME}:PostUser not set.");
            if (string.IsNullOrEmpty(postFoodRestrictions))
                throw new KeyNotFoundException($"Config key {SECTION_NAME}:PostFoodRestrictions not set.");
        }
    }
}
