using FpElectionCalculator.Domain.JsonModels;
using FpElectionCalculator.Domain.Services;
using Xunit;

namespace FpElectionCalculator.Domain.Tests
{
    public class WebserviceDataParserCandidatesTests
    {
        private CandidatesList Execute(string json)
        {
            return WebserviceDataParser.GetCandidatesList(json);
        }

        [Fact]
        public void ParsesJsonResponse()
        {
            var ex = Record.Exception(() => Execute(EmbeddedResources.GetResource("candidates.json")));
            Assert.Null(ex);
        }

    }
}
