using Newtonsoft.Json;

namespace ControleApData.Client.Models
{
    public class BasicResponseModel
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
