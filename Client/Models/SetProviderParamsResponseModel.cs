using Newtonsoft.Json;

namespace ApdataTimecardFixer.Client.Models
{
    public class SetProviderParamsResponseModel
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
