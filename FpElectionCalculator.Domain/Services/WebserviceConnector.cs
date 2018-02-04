using System.Collections.Generic;
using FpElectionCalculator.Domain.JsonModels;
using System.Linq;

namespace FpElectionCalculator.Domain.Services
{
    internal static class WebserviceConnector
    {
        internal static bool IsPeselDisallowedToVote(string pesel)
        {
            string json = WebserviceRawCommunication.GetPeopleDisallowedToVote();
            IList<Person> disallowedPersonList = WebserviceDataParser.GetDisallowedPeopleList(json).PersonList;
            return disallowedPersonList.Any(p => p.Pesel == pesel);
        }

        internal static CandidatesList GetCandidatesList()
        {
            return WebserviceDataParser.GetCandidatesList(WebserviceRawCommunication.GetCandidates());
        }
    }
}
