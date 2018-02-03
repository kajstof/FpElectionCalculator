using FpElectionCalculator.Domain.Services;
using Xunit;
using Xunit.Abstractions;

namespace FpElectionCalculator.Domain.Tests
{
    public class WebserviceRawCommunicationDisallowedTests
    {
        private readonly ITestOutputHelper output;

        public WebserviceRawCommunicationDisallowedTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        private string Execute(bool xml = false)
        {
            return new WebserviceRawCommunication(xml).GetPeopleDisallowedToVote();
        }

        [Fact]
        public void ReturnJsonResponse()
        {
            string results = Execute();
            output.WriteLine($"Return response:\n{results}");
            Assert.NotEmpty(results);
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
