using Dadata.SmallApiClient.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Dadata.SmallApiClient
{
    public class DadataClient : IDadataClient
    {
        private HttpClient _httpClient { get; }
        public DadataClient(IOptions<DadataServiceConfig> config)
        {
            if (string.IsNullOrWhiteSpace(config.Value.Token))
                throw new Exception("Dadata service 'token' is null or empty!");
            if (string.IsNullOrWhiteSpace(config.Value.Secret))
                throw new Exception("Dadata service 'secret' is null or empty!");

            _httpClient = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip
            });
            _httpClient.DefaultRequestHeaders.Accept.Add(
                MediaTypeWithQualityHeaderValue.Parse("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", config.Value.Token);
            _httpClient.DefaultRequestHeaders.Add("X-Secret", config.Value.Secret);
        }
        public async Task<TResponce> Execute<TResponce>(HttpMethod method, string url, object query = null)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new Exception("Dadata http request url is null or empty!");

            var httpRequestMessage = new HttpRequestMessage(method, url);

            if (query != null)
                httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");

            using (var response = await _httpClient.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false))
            {
                var result = await response.Content.ReadAsStringAsync();

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return JsonConvert.DeserializeObject<TResponce>(string.IsNullOrEmpty(result) ? null : result);
                    case HttpStatusCode.PaymentRequired:
                        throw new Exception(nameof(HttpStatusCode.PaymentRequired));
                    case HttpStatusCode.Forbidden:
                        throw new Exception(nameof(HttpStatusCode.Forbidden));
                    case HttpStatusCode.MethodNotAllowed:
                        throw new Exception(nameof(HttpStatusCode.MethodNotAllowed));
                    case HttpStatusCode.RequestEntityTooLarge:
                        throw new Exception(nameof(HttpStatusCode.RequestEntityTooLarge));
                    default:
                        throw new Exception($"{response.StatusCode.ToString()}: {result}");
                }
            }
        }

    }
}
