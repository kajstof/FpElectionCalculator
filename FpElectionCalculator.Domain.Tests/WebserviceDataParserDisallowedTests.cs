using FpElectionCalculator.Domain.JsonModels;
using FpElectionCalculator.Domain.Services;
using Xunit;

namespace FpElectionCalculator.Domain.Tests
{
    public class WebserviceDataParserDisallowedTests
    {
        private DisallowedList Execute(string json)
        {
            return WebserviceDataParser.GetDisallowedPeopleList(json);
        }

        [Fact]
        public void ParsesJsonResponse()
        {
            var ex = Record.Exception(() => Execute(EmbeddedResources.GetResource("disallowed.json")));
            Assert.Null(ex);
        }
    }
}
