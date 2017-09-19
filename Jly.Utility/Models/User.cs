using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Models
{
    public class User
    {
        [JsonProperty("tk")]
        public string Token { get; set; }

        [JsonProperty("userInfo")]
        public UserInfo UserInfo { get; set; }

        [JsonProperty("oparkRole")]
        public OparkRole Role { get; set; }
    }
}
