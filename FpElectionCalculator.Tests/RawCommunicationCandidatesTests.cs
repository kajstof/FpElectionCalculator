﻿using FpElectionCalculator.Domain.JsonModels;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace FpElectionCalculator.Tests
{
    public class RawCommunicationCandidatesTests
    {
        private readonly ITestOutputHelper output;

        public RawCommunicationCandidatesTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        private string Execute(bool xml = false)
        {
            return new RawCommunication(xml).getCandidates();
        }

        [Fact]
        public void ReturnJsonResponse()
        {
            string results = Execute();
            output.WriteLine($"Return response:\n{results}");
            Assert.NotEmpty(results);
        }

        [Fact]
        public void ParseJsonResponse()
        {
            string json = Execute();
            var ex = Record.Exception(() => JObject.Parse(json).SelectToken("candidates").ToObject<CandidatesList>());
            Assert.Null(ex);
        }

        [Fact]
        public void ReturnXmlResponse()
        {
            string results = Execute(true);
            output.WriteLine($"Return response:\n{results}");
            Assert.NotEmpty(results); ;
        }
    }
}
