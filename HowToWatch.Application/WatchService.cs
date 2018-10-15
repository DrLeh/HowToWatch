using HowToWatch.Extensions;
using HowToWatch.JustWatch;
using HowToWatch.Models;
using HowToWatch.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace HowToWatch.Application
{
    public class WatchService
    {
        public ISourceService SourceService { get; }
        public IUserService UserService { get; }

        public WatchService(ISourceService sourceService, IUserService userService)
        {
            SourceService = sourceService;
            UserService = userService;
        }


        public string GetHowToWatch(string query)
        {
            var response = SourceService.Query(query);
            return Parse(query, response);
        }

        public string GetHowToWatch(string query, long userId)
        {
            var response = SourceService.Query(query);
            return Parse(query, userId, response);
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
            var flatrate = item.Offers.Where(x => x.MonetizationType == MonetizationType.FlatRate)
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
                return GetNiceStringForList($"{item.Title} can be watched for free on ", t1Services);
            }
            //todo: expand this to return info about where else you can find it.
            //todo: make configurable so you can say if you care about other services

            return $"{item.Title} is not available on any flat rate streaming services";
        }


        private string Parse(string query, long userId, SourceResponse response)
        {
            if (UserService == null)
                throw new InvalidOperationException("Must provide an IUserService");

            var item = response.Items.FirstOrDefault();

            var noResMessage = $"Sorry, I couldn't find any results for '{query}'";
            if (item == null)
                return noResMessage;

            if (item.Offers == null || !item.Offers.Any())
                return noResMessage;

            var services = UserService.GetUserServicePreferences(userId);

            var avoidedServiceUrls = services
                .Where(x => x.Preference == -1)
                .SelectMany(x => x.Service.Urls.Select(y => y.Url.ToLower()))
                .ToList();

            //for now, only use flatrate
            var flatrate = item.Offers.Where(x => x.MonetizationType == MonetizationType.FlatRate)
                .Select(x => x.Urls?.StandardWeb)
                .Where(x => x != null)
                .Select(x => x.ToLower())
                .Where(x => !avoidedServiceUrls.Contains(x))
                .ToList()
                ;

            var preferredServices = services
                .Where(x => x.Preference > 0)
                .ToList();

            var serviceList = preferredServices
                .OrderBy(x => x.Preference)
                .Where(x => x.Service.Urls.Any(y => flatrate.Contains(y.Url)))
                .Select(x => x.Service)
                .ToList();

            var serviceNames = serviceList.OrEmptyIfNull().Select(x => x.Name).ToList();
            var prefServiceUrls = serviceList.SelectMany(z => z.Urls.Select(y => y.Url));

            var remainingServices = flatrate.Where(x => !prefServiceUrls.Contains(x));
            if (remainingServices.Any())
                serviceNames.AddRange(remainingServices);

            if (serviceNames.Any())
                return GetNiceStringForList($"{item.Title} can be watched for free on", serviceNames);

            return $"{item.Title} is not available on any of your flat rate streaming services";
        }


        public string GetNiceStringForList(string baseString, IEnumerable<string> list)
        {
            var distinct = list.Distinct();

            var ret = baseString;
            var joined = string.Join(", ", distinct);
            var last = distinct.Last();
            var niceified = joined.Replace($", {last}", $" and {last}");
            return (baseString + " " + niceified).Replace("  ", " ");
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

