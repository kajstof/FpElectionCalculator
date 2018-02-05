using FpElectionCalculator.Domain.Services;
using Xunit;
using Xunit.Abstractions;

namespace FpElectionCalculator.Domain.Tests
{
    public class WebserviceRawCommunicationCandidatesTests
    {
        private readonly ITestOutputHelper _output;
        private readonly WebserviceRawCommunication _webservice;

        public WebserviceRawCommunicationCandidatesTests(ITestOutputHelper output)
        {
            _output = output;
            _webservice = new WebserviceRawCommunication();
        }

        private string Execute(bool xml = false)
        {
            return _webservice.GetCandidates();
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
