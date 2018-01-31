using FpElectionCalculator.Domain;
using Xunit;
using Xunit.Abstractions;

namespace FpElectionCalculator.Tests
{
    public class WebserviceRawCommunicationCandidatesTests
    {
        private readonly ITestOutputHelper output;

        public WebserviceRawCommunicationCandidatesTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        private string Execute(bool xml = false)
        {
            return new WebserviceRawCommunication(xml).getCandidates();
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
