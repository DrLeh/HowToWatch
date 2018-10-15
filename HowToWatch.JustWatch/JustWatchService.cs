using HowToWatch.JustWatch;
using HowToWatch.Models;
using HowToWatch.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace HowToWatch.JustWatch
{
    public class JustWatchService : ISourceService
    {
        private HttpClient _client;
        private HttpClient Client => _client ?? (_client = new HttpClient());

        public SourceResponse Query(string query)
        {
            var encoded = HttpUtility.UrlEncode(query);
            var baseUrl = "https://apis.justwatch.com";
            var url = $"{baseUrl}/content/titles/en_US/popular?body=%7B%22content_types%22:%5B%22show%22,%22movie%22%5D,%22page%22:1,%22page_size%22:1,%22query%22:%22{encoded}%22%7D";

            var response = Client.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var justWatchResponse = JsonConvert.DeserializeObject<JustWatchResponse>(result);

            var mapper = new JustWatchResponseMapper();
            return mapper.ToModel(justWatchResponse);
        }
    }
}
