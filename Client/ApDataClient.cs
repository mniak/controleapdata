using ApdataTimecardFixer.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApdataTimecardFixer.Client
{
    public class ApDataClient
    {
        private readonly ApDataLowLevelClient lowLevelClient;

        public ApDataClient(ApDataLowLevelClient lowLevelClient)
        {
            this.lowLevelClient = lowLevelClient ?? throw new ArgumentNullException(nameof(lowLevelClient));
        }

        public string Ts { get; set; }
        public string SessionId { get; set; }

        public async Task<LoginResponseModel> Login(string username, string password)
        {
            var formdata = new Dictionary<string, string>()
            {
                ["username"] = username,
                ["password"] = password,
            };
            var result = await lowLevelClient.PostWithBodyForm<LoginResponseModel>("/Login", formdata);
            if (result.Success)
            {
                SessionId = result.SessionId;
                Ts = await lowLevelClient.GetCookieAsync("ts");
            }
            return result;
        }

        public async Task<SetProviderParamsResponseModel> SetProviderParams(long hwd, int year, int month, long employeeCode)
        {
            var formdata = new Dictionary<string, string>()
            {
                ["hwd"] = hwd.ToString(),
                ["Field_0"] = year.ToString(),
                ["Field_0_TP"] = "string",
                ["Field_1"] = month.ToString(),
                ["Field_1_TP"] = "string",
                ["Field_2"] = employeeCode.ToString(),
                ["Field_2_TP"] = "int",
                ["tsc"] = Ts,
                ["sessionID"] = SessionId,
            };
            var result = await lowLevelClient.PostWithBodyForm<SetProviderParamsResponseModel>("/SetProviderParams", formdata);
            return result;
        }

        public async Task<BasicResponseModel> UpdateProviderRecord(long hwd, string dateString, DateTime date, StatusDoDia status, string start, string end)
        {
            const int REASON = 10004;

            var formdata = new Dictionary<string, string>()
            {
                ["hwd"] = hwd.ToString(),
                ["transactionID"] = "30143",
                ["IndexField_0"] = "7",
                ["IndexField_1"] = "15",
                ["Field_0_TP"] = "string",
                ["Field_0"] = start,
                ["Field_0_Index"] = "7",
                ["Field_1_TP"] = "int",
                ["Field_1"] = REASON.ToString(),
                ["Field_1_Index"] = "10",
                ["Field_2_TP"] = "string",
                ["Field_2"] = end,
                ["Field_2_Index"] = "15",
                ["Field_3_TP"] = "int",
                ["KeyField_0"] = "false",
                ["KeyField_0_TP"] = "boolean",
                ["KeyField_1"] = dateString,
                ["KeyField_1_TP"] = "string",
                ["KeyField_2"] = date.ToString("yyyy-MM-ddT00:00"),
                ["KeyField_2_TP"] = "date",
                ["tsc"] = Ts,
                ["sessionID"] = SessionId,
            };

            var result = await lowLevelClient.PostWithBodyForm<BasicResponseModel>("/updateProviderRecord", formdata);
            return result;
        }

        public async Task<CreateEditGridAndGetHeadersResponseModel> CreateEditGridAndGetHeaders(string @class)
        {
            var querydata = new Dictionary<string, string>()
            {
                ["cls"] = @class,
                ["tsc"] = Ts,
                ["sessionID"] = SessionId,
            };
            var result = await lowLevelClient.GetWithQueryParams<CreateEditGridAndGetHeadersResponseModel>("/CreateEditGridAndGetHeaders", querydata);
            return result;
        }
        public async Task<GetEditGridPageResponseModel> GetEditGridPage(long hwd)
        {
            var querydata = new Dictionary<string, string>()
            {
                ["Hwd"] = hwd.ToString(),
                ["tsc"] = Ts,
                ["sessionID"] = SessionId,
                ["start"] = 0.ToString(),
                ["limit"] = 1000.ToString(),
            };
            var result = await lowLevelClient.GetWithQueryParams<GetEditGridPageResponseModel>("/GetEditGridPage", querydata);
            return result;
        }
    }
}
