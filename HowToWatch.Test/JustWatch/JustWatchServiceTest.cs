using System;
using System.Collections.Generic;
using FluentAssertions;
using HowToWatch;
using HowToWatch.Application;
using HowToWatch.JustWatch;
using HowToWatch.Models;
using HowToWatch.Services;
using Moq;
using NUnit.Framework;

namespace HowToWatch.Test.Services
{
    [TestFixture]
    public class JustWatchServiceTest
    {
        //this is a sanity test just to ensure JustWatch is working. Should not be enabled
        // as availability will change and this cannot be asserted.
        //[Test]
        //public void Parse_Test()
        //{
        //    var input = "hot fuzz";
        //    var jwService = new JustWatchService();

        //    var service = new WatchService(jwService);

        //    var result = service.GetHowToWatch(input);

        //    //this is liable to break as availability changes.
        //    result.Should().Be("Hot Fuzz can be watched for free on netflix");
        //}
    }
}
