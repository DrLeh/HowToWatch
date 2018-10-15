using HowToWatch.Mappers;
using HowToWatch.Models;
using HowToWatch.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowToWatch.JustWatch
{
    public class JustWatchResponseMapper : BaseMapper<SourceResponse, JustWatchResponse>
    {
        public override SourceResponse ToModel(JustWatchResponse view)
        {
            var response = new SourceResponse
            {
                Items = view.Items.OrEmptyIfNull()
                    .Select(x => new WatchItem
                    {
                        Title = x.Title,
                        Offers = x.Offers.OrEmptyIfNull()
                            .Select(o => new Offer
                            {
                                MonetizationType = o.MonetizationType,
                                Urls = new SourceUrls
                                {
                                    StandardWeb = o.Urls?.StandardWeb?.AbsoluteUri
                                }
                            })
                            .ToList()
                    })
                    .ToList()
            };
            return response;
        }
    }
}
