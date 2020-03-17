using Newtonsoft.Json;

namespace ControleApData.Client.Models
{
    public class SetProviderParamsResponseModel
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
