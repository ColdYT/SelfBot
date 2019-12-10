using Newtonsoft.Json;

namespace xD
{
    public class Config
    {
        [JsonProperty("auth_prefix")]
        public string Prefix { get; set; }
    }
}
