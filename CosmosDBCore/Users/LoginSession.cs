using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosDBRepository.Users
{
    public class LoginSession
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        public string UserId { get; set; }

        public DateTime CreationTime { get; set; }
        public DateTime? LogoutTime { get; set; }
    }
}
