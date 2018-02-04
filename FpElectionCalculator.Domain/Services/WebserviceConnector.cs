using FpElectionCalculator.Domain.JsonModels;
using System.Linq;

namespace FpElectionCalculator.Domain.Services
{
    internal static class WebserviceConnector
    {
        internal static bool IsPeselDisallowedToVote(string pesel)
        {
            string json = new WebserviceRawCommunication().GetPeopleDisallowedToVote();
            var disallowedPersonList = new WebserviceDataParser().GetDisallowedPeopleList(json).PersonList;
            return disallowedPersonList.Any(p => p.Pesel == pesel);
        }

        internal static CandidatesList GetCandidatesList()
        {
            WebserviceRawCommunication webservice = new WebserviceRawCommunication();
            WebserviceDataParser parser = new WebserviceDataParser();
            return parser.GetCandidatesList(webservice.GetCandidates());
        }
    }
}
