using HowToWatch.Core.Data;
using HowToWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowToWatch.Services
{
    public interface IStreamingServiceService
    {
        IEnumerable<Service> GetServiceByUrl(string url);
    }

    public class StreamingServiceService : IStreamingServiceService
    {
        public StreamingServiceService(IRepository repository)
        {
            Repository = repository;
        }

        public IRepository Repository { get; }

        public IEnumerable<Service> GetServiceByUrl(string url)
        {
            return Repository.Services
                .Where(x => x.Urls.Any(y => y.Url.Contains(url)))
                .ToList();
        }
    }
}
