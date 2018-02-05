using FpElectionCalculator.Domain.JsonModels;
using FpElectionCalculator.Domain.Services;
using Xunit;

namespace FpElectionCalculator.Domain.Tests
{
    public class WebserviceDataParserCandidatesTests
    {
        private readonly WebserviceDataParser _parser;

        public WebserviceDataParserCandidatesTests()
        {
            _parser = new WebserviceDataParser();
        }

        private CandidateList Execute(string json)
        {
            return _parser.GetCandidatesList(json);
        }

        [Fact]
        public void ParsesJsonResponse()
        {
            var ex = Record.Exception(() => Execute(EmbeddedResources.GetResource("dbCandidates.json")));
            Assert.Null(ex);
        }

    }
}
