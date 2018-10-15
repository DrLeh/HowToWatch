using HowToWatch.JustWatch;
using HowToWatch.Models;
using HowToWatch.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace HowToWatch.Application
{
    public class WatchService
    {
        public ISourceService SourceService { get; }
        public WatchService(ISourceService sourceService)
        {
            SourceService = sourceService;
        }


        public string GetHowToWatch(string query)
        {
            var response = SourceService.Query(query);
            return Parse(query, response);
        }

        private string Parse(string query, SourceResponse response)
        {
            var item = response.Items.FirstOrDefault();

            var noResMessage = $"Sorry, I couldn't find any results for '{query}'";
            if (item == null)
                return noResMessage;

            if (item.Offers == null || !item.Offers.Any())
                return noResMessage;

            var t1Services = new List<string>();
            var flatrate = item.Offers.Where(x => x.MonetizationType == "flatrate")
                .Select(x => x.Urls?.StandardWeb)
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

            return $"{item.Title} is not available on any flat rate streaming services";
        }


        public List<string> T1Services = new List<string>
        {
            "netflix",
            "hulu",
            "amazon",
        };

        public List<string> T2Services = new List<string>
        {
            "hulu",
            "youtube",
        };
    }
}

