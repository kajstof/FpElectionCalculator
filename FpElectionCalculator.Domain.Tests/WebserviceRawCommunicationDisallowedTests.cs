using FpElectionCalculator.Domain.Services;
using Xunit;
using Xunit.Abstractions;

namespace FpElectionCalculator.Domain.Tests
{
    public class WebserviceRawCommunicationDisallowedTests
    {
        private readonly ITestOutputHelper _output;

        public WebserviceRawCommunicationDisallowedTests(ITestOutputHelper output)
        {
            _output = output;
        }

        private string Execute(bool xml = false)
        {
            return WebserviceRawCommunication.GetPeopleDisallowedToVote();
        }

        [Fact]
        public void ReturnJsonResponse()
        {
            string results = Execute();
            _output.WriteLine($"Return response:\n{results}");
            Assert.NotEmpty(results);
        }

        [Fact]
        public void ReturnXmlResponse()
        {
            string results = Execute(true);
            _output.WriteLine($"Return response:\n{results}");
            Assert.NotEmpty(results);
        }
    }
}
