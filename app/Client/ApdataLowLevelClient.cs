using ApdataTimecardFixer.Client.Parser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApdataTimecardFixer.Client
{
    public class ApdataLowLevelClient
    {
        const string BasePath = ".net/index.ashx";

        private readonly Arguments args;
        private readonly HttpClient httpClient;
        private readonly CookieContainer cookies;

        public ApdataLowLevelClient(Arguments args, HttpClient httpClient, CookieContainer cookies)
        {
            this.args = args ?? throw new ArgumentNullException(nameof(args));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.cookies = cookies;
        }

        private static TResponse DeserializeValue<TResponse>(string content)
        {
            var json = NormalizeJson(content);
            var result = JsonConvert.DeserializeObject<TResponse>(json);
            return result;
        }

        private static string NormalizeJson(string content)
        {
            var oldJson = JSON.Parse(content);
            var newJson = oldJson.ToJSON();
            return newJson;
        }

        public async Task<TResponse> PostWithBodyForm<TResponse>(string path, IDictionary<string, string> form)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, await GetUrlAsync(path));
            request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
            request.Content = new FormUrlEncodedContent(form);

            var response = await httpClient.SendAsync(request);
            var responseContentString = await response.Content.ReadAsStringAsync();

            var result = DeserializeValue<TResponse>(responseContentString);
            return result;
        }

        public async Task<TResponse> GetWithQueryParams<TResponse>(string path, IDictionary<string, string> query)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, await GetUrlAsync(path, query));
            request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");

            var response = await httpClient.SendAsync(request);
            var responseContentString = await response.Content.ReadAsStringAsync();

            var result = DeserializeValue<TResponse>(responseContentString);
            return result;
        }

        public async Task<string> GetCookieAsync(string cookieName)
        {
            var c = cookies.GetCookies(new Uri(await GetUrlAsync()));
            var value = c[cookieName]?.Value;
            return value;
        }

        private async Task<string> GetUrlAsync(string path = null, IDictionary<string, string> query = null)
        {
            var baseUrl = args.BaseUrl != null 
                ? args.BaseUrl
                : $"https://cliente.apdata.com.br/{args.Company}";
                
            var url = $"{baseUrl}/.net/index.ashx{path}";
            if (query == null || !query.Any())
                return url;

            var queryString = await new FormUrlEncodedContent(query).ReadAsStringAsync();
            return $"{url}?{queryString}";
        }
    }
}
