using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.JsonModels;
using FpElectionCalculator.Domain.Services;
using System.Collections.Generic;
using Xunit;

namespace FpElectionCalculator.Domain.Tests
{
    public class JsonToDbConverterTests
    {
        private IList<Party> Execute()
        {
            WebserviceRawCommunication webservice = new WebserviceRawCommunication();
            WebserviceDataParser parser = new WebserviceDataParser();
            CandidatesList candidatesList = parser.GetCandidatesList(EmbeddedResources.GetResource("disallowed.json"));

            JsonToDbConverter jsonConverter = new JsonToDbConverter();
            return jsonConverter.ConvertPartiesAndCandidatesFromJsonModelToDbModel(candidatesList);
        }

        [Fact]
        public void ReturnIListPartyObject()
        {
            IList<Party> parties = Execute();
        }
    }
}
