using FpElectionCalculator.Domain;
using FpElectionCalculator.Domain.JsonModels;
using Xunit;

namespace FpElectionCalculator.Tests
{
    public class WebserviceDataParserCandidatesTests
    {
        private WebserviceDataParser _parser;

        public WebserviceDataParserCandidatesTests()
        {
            _parser = new WebserviceDataParser();
        }

        private CandidatesList Execute(string json)
        {
            return _parser.GetCandidatesList(json);
        }

        [Fact]
        public void ParsesJsonResponse()
        {
            var ex = Record.Exception(() => Execute(EmbeddedResources.GetResource("candidates.json")));
            Assert.Null(ex);
        }

    }
}
