using FpElectionCalculator.Domain.JsonModels;
using System.Collections.Generic;
using System.Linq;

namespace FpElectionCalculator.Domain.Services
{
    public class WebserviceLogic
    {
        internal static bool IsPeselDisallowedToVote(string pesel)
        {
            string json = new WebserviceRawCommunication().GetPeopleDisallowedToVote();
            IList<Person> disallowedPersonList = new WebserviceDataParser().GetDisallowedPeopleList(json).PersonList;
            return disallowedPersonList.Any(p => p.Pesel == pesel);
        }

        internal static CandidatesList GetCandidatesList()
        {
            return new WebserviceDataParser().GetCandidatesList(new WebserviceRawCommunication().GetCandidates());
        }
    }
}
