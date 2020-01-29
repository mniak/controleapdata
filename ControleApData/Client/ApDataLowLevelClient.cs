using ControleApData.Client.Parser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ControleApData.Client
{
    public class ApDataLowLevelClient
    {
        private readonly HttpClient httpClient;
        private readonly CookieContainer cookies;
        private readonly string baseUrl;

        public string GetCookie(string cookieName)
        {
            var c = cookies.GetCookies(new Uri(baseUrl));
            var value = c[cookieName]?.Value;
            return value;
        }

        public ApDataLowLevelClient(HttpClient httpClient, CookieContainer cookies, GetApDataBaseUrl getApDataBaseUrl)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.cookies = cookies;
            if (getApDataBaseUrl == null)
                throw new ArgumentNullException(nameof(getApDataBaseUrl));
            baseUrl = getApDataBaseUrl();
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
            var url = $"{baseUrl}{path}";
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
            request.Content = new FormUrlEncodedContent(form);

            var response = await httpClient.SendAsync(request);
            var responseContentString = await response.Content.ReadAsStringAsync();

            var result = DeserializeValue<TResponse>(responseContentString);
            return result;
        }

        public async Task<TResponse> GetWithQueryParams<TResponse>(string path, IDictionary<string, string> query)
        {
            var queryString = await new FormUrlEncodedContent(query).ReadAsStringAsync();

            var url = $"{baseUrl}{path}?{queryString}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");

            var response = await httpClient.SendAsync(request);
            var responseContentString = await response.Content.ReadAsStringAsync();

            var result = DeserializeValue<TResponse>(responseContentString);
            return result;
        }
    }
}
