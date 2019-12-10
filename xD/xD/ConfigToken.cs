using Newtonsoft.Json;

namespace xD
{
    public class ConfigToken
    {
        [JsonProperty("auth_token")]
        public string Token { get; set; }
    }
}
