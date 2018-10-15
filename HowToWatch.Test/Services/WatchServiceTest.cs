using System;
using System.Collections.Generic;
using FluentAssertions;
using HowToWatch;
using HowToWatch.Application;
using HowToWatch.Models;
using HowToWatch.Services;
using Moq;
using NUnit.Framework;

namespace HowToWatch.Test.Services
{
    [TestFixture]
    public class WatchServiceTest
    {
        [Test]
        public void Parse_Test()
        {
            var sourceService = new Mock<ISourceService>();
            var input = "hot fuzz";
            sourceService.Setup(x => x.Query(input)).Returns(new SourceResponse
            {
                Items = new List<WatchItem>
                {
                    new WatchItem
                    {
                        Title = "Hot Fuzz",
                        Offers = new List<Offer>
                        {
                            new Offer
                            {
                                MonetizationType = MonetizationType.FlatRate,
                                Urls = new SourceUrls
                                {
                                    StandardWeb = "amazon.com"
                                }
                            }
                        }
                    }
                }
            });

            var service = new WatchService(sourceService.Object);

            var result = service.GetHowToWatch(input);

            result.Should().Be("Hot Fuzz can be watched for free on amazon");
        }

        [Test]
        public void Parse_NoResult()
        {
            var sourceService = new Mock<ISourceService>();
            var input = "hot fuzz";
            sourceService.Setup(x => x.Query(input)).Returns(new SourceResponse
            {
                Items = new List<WatchItem>
                {
                    new WatchItem
                    {
                        Title = "Hot Fuzz",
                        Offers = new List<Offer>
                        {
                            new Offer
                            {
                                MonetizationType = MonetizationType.FlatRate,
                                Urls = new SourceUrls
                                {
                                    StandardWeb = "SomeWeirdSite.com"
                                }
                            }
                        }
                    }
                }
            });

            var service = new WatchService(sourceService.Object);

            var result = service.GetHowToWatch(input);

            result.Should().Contain("Hot Fuzz is not available on any flat rate streaming services");
        }
    }
}
