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

            var service = new WatchService(sourceService.Object, null);

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

            var service = new WatchService(sourceService.Object, null);

            var result = service.GetHowToWatch(input);

            result.Should().Contain("Hot Fuzz is not available on any flat rate streaming services");
        }

        [Test]
        public void Parse_Multiple()
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
                                    StandardWeb = "netflix"
                                }
                            },
                            new Offer
                            {
                                MonetizationType = MonetizationType.FlatRate,
                                Urls = new SourceUrls
                                {
                                    StandardWeb = "amazon"
                                }
                            },
                        },
                    }
                }
            });

            var service = new WatchService(sourceService.Object, null);

            var result = service.GetHowToWatch(input);

            result.Should().Be("Hot Fuzz can be watched for free on netflix and amazon");
        }

        [Test]
        public void Parse_AvoidService()
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
                                    StandardWeb = "netflix"
                                }
                            },
                            new Offer
                            {
                                MonetizationType = MonetizationType.FlatRate,
                                Urls = new SourceUrls
                                {
                                    StandardWeb = "amazon"
                                }
                            },
                        },
                    }
                }
            });

            var userService = new Mock<IUserService>();
            userService.Setup(x => x.GetUserServicePreferences(It.IsAny<long>()))
                .Returns(new[]
                {
                    new UserServicePreference
                    {
                        MonetizationType = new MonetizationType
                        {
                            TypeName = MonetizationType.FlatRate
                        },
                        Service = new Service
                        {
                            Name = "amazon",
                            Urls =
                            {
                                new ServiceUrl
                                {
                                    Url = "amazon" //todo: fix so .com works
                                }
                            }
                        },
                        Preference = -1,
                    }
                });

            var service = new WatchService(sourceService.Object, userService.Object);

            var result = service.GetHowToWatch(input, 1);

            result.Should().Be("Hot Fuzz can be watched for free on netflix");
        }

        [Test]
        public void Parse_Prefer_Service()
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
                                    StandardWeb = "netflix"
                                }
                            },
                            new Offer
                            {
                                MonetizationType = MonetizationType.FlatRate,
                                Urls = new SourceUrls
                                {
                                    StandardWeb = "amazon"
                                }
                            },
                        },
                    }
                }
            });

            var userService = new Mock<IUserService>();
            userService.Setup(x => x.GetUserServicePreferences(It.IsAny<long>()))
                .Returns(new[]
                {
                    new UserServicePreference
                    {
                        Preference = 1,
                        MonetizationType = new MonetizationType
                        {
                            TypeName = MonetizationType.FlatRate
                        },
                        Service = new Service
                        {
                            Name = "amazon",
                            Urls =
                            {
                                new ServiceUrl
                                {
                                    Url = "amazon" //todo: fix so .com works
                                }
                            }
                        },
                    },
                    new UserServicePreference
                    {
                        Preference = 2,
                        MonetizationType = new MonetizationType
                        {
                            TypeName = MonetizationType.FlatRate
                        },
                        Service = new Service
                        {
                            Name = "netflix",
                            Urls =
                            {
                                new ServiceUrl
                                {
                                    Url = "netflix" //todo: fix so .com works
                                }
                            }
                        },
                    },
                });

            var service = new WatchService(sourceService.Object, userService.Object);

            var result = service.GetHowToWatch(input, 1);

            //order of result is changed based on the provided user preference
            result.Should().Be("Hot Fuzz can be watched for free on amazon and netflix");
        }

        [Test]
        public void Parse_No_Services()
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
                                    StandardWeb = "netflix"
                                }
                            },
                            new Offer
                            {
                                MonetizationType = MonetizationType.FlatRate,
                                Urls = new SourceUrls
                                {
                                    StandardWeb = "amazon"
                                }
                            },
                        },
                    }
                }
            });

            var userService = new Mock<IUserService>();
            userService.Setup(x => x.GetUserServicePreferences(It.IsAny<long>()))
                .Returns(new[]
                {
                    new UserServicePreference
                    {
                        Preference = -1,
                        MonetizationType = new MonetizationType
                        {
                            TypeName = MonetizationType.FlatRate
                        },
                        Service = new Service
                        {
                            Name = "amazon",
                            Urls =
                            {
                                new ServiceUrl
                                {
                                    Url = "amazon" //todo: fix so .com works
                                }
                            }
                        },
                    },
                    new UserServicePreference
                    {
                        Preference = -1,
                        MonetizationType = new MonetizationType
                        {
                            TypeName = MonetizationType.FlatRate
                        },
                        Service = new Service
                        {
                            Name = "netflix",
                            Urls =
                            {
                                new ServiceUrl
                                {
                                    Url = "netflix" //todo: fix so .com works
                                }
                            }
                        },
                    },
                });

            var service = new WatchService(sourceService.Object, userService.Object);

            var result = service.GetHowToWatch(input, 1);

            //order of result is changed based on the provided user preference
            result.Should().Be("Hot Fuzz is not available on any of your flat rate streaming services");
        }
    }
}
