using HowToWatch.JustWatch;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace HowToWatch
{
    public static class WatchUtility
    {
        private static HttpClient Client => _client ?? (_client = new HttpClient());
        private static HttpClient _client;

        public static string GetHowToWatch(string query)
        {
            var encoded = HttpUtility.UrlEncode(query);
            var url = $"https://apis.justwatch.com/content/titles/en_US/popular?body=%7B%22content_types%22:%5B%22show%22,%22movie%22%5D,%22page%22:1,%22page_size%22:1,%22query%22:%22{encoded}%22%7D";

            var response = Client.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var justWatchResponse = JsonConvert.DeserializeObject<JustWatchResponse>(result);
            return Parse(query, justWatchResponse);
        }

        private static string Parse(string query, JustWatchResponse response)
        {
            var item = response.Items.FirstOrDefault();

            var noResMessage = $"Sorry, I couldn't find any results for '{query}'";
            if (item == null)
                return noResMessage;

            if (item.Offers == null || !item.Offers.Any())
                return noResMessage;

            var t1Services = new List<string>();
            var flatrate = item.Offers.Where(x => x.MonetizationType == "flatrate")
                .Select(x => x.Urls?.StandardWeb?.AbsoluteUri)
                .Where(x => x != null);

            //t1 = flatrate ones most ppl have
            //could be linqified but whatever for now
            foreach (var flat in flatrate)
            {
                t1Services.AddRange(T1Services.Where(x => flat.Contains(x)));
            }

            t1Services = t1Services.Distinct().ToList();
            if (t1Services.Any())
            {
                var ret = $"{item.Title} can be watched for free on ";
                var joined = string.Join(", ", t1Services);
                var last = t1Services.Last();
                var niceified = joined.Replace($", {last}", $" and {last}");
                ret = ret + niceified;
                return ret;
            }
            //todo: expand this to return info about where else you can find it.
            //todo: make configurable so you can say if you care about other services

            return $"{item.Title} is not available on any flatrate streaming services";
        }


        public static List<string> T1Services = new List<string>
        {
            "netflix",
            "hulu",
            "amazon",
        };

        public static List<string> T2Services = new List<string>
        {
            "hulu",
            "youtube",
        };

        public static string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return string.Join("&", properties.ToArray());
        }
    }
}

