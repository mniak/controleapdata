using Newtonsoft.Json;

namespace ControleApData.Client.Models
{
    public class LoginResponseModel
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("SessionID")]
        public string SessionId { get; set; }

        [JsonProperty("selectedEmployee")]
        public long SelectedEmployee { get; set; }

        [JsonProperty("selectedCandidate")]
        public long SelectedCandidate { get; set; }

        [JsonProperty("selectedPosition")]
        public long SelectedPosition { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("userEmployee")]
        public long UserEmployee { get; set; }

        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("languages")]
        public Languages Languages { get; set; }

        [JsonProperty("initalURL")]
        public string InitalUrl { get; set; }

        [JsonProperty("NTLM")]
        public bool Ntlm { get; set; }

        [JsonProperty("cosDiretorioPersWeb2")]
        public string CosDiretorioPersWeb2 { get; set; }

        [JsonProperty("selectedVacancy")]
        public long SelectedVacancy { get; set; }

        [JsonProperty("selectedEmployeeName")]
        public string SelectedEmployeeName { get; set; }

        [JsonProperty("selectedCandidateName")]
        public string SelectedCandidateName { get; set; }

        [JsonProperty("selectedVacancyDescr")]
        public string SelectedVacancyDescr { get; set; }

        [JsonProperty("upperCase")]
        public string UpperCase { get; set; }

        [JsonProperty("acceptTerm")]
        public string AcceptTerm { get; set; }

        [JsonProperty("useTerm")]
        public string UseTerm { get; set; }

        [JsonProperty("currentCountry")]
        public long CurrentCountry { get; set; }

        [JsonProperty("idTipoDaltonismo")]
        public long IdTipoDaltonismo { get; set; }

        [JsonProperty("totalFolhasCC")]
        public long TotalFolhasCc { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("details")]
        public string[] Details { get; set; }

        [JsonProperty("errorClearCode")]
        public string[] ErrorClearCode { get; set; }
    }

    public partial class Languages
    {
        [JsonProperty("port")]
        public bool Port { get; set; }
    }
}
