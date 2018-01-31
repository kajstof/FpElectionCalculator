using FpElectionCalculator.Domain;
using FpElectionCalculator.Domain.JsonModels;
using Xunit;

namespace FpElectionCalculator.Tests
{
    public class WebserviceDataParserDisallowedTests
    {
        private WebserviceDataParser _parser;

        public WebserviceDataParserDisallowedTests()
        {
            _parser = new WebserviceDataParser();
        }

        private DisallowedList Execute(string json)
        {
            return _parser.GetDisallowedPeopleList(json);
        }

        [Fact]
        public void ParsesJsonResponse()
        {
            var ex = Record.Exception(() => Execute(EmbeddedResources.GetResource("disallowed.json")));
            Assert.Null(ex);
        }
    }
}
