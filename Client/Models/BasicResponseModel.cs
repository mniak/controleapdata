using Newtonsoft.Json;

namespace ApdataTimecardFixer.Client.Models
{
    public class BasicResponseModel
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
