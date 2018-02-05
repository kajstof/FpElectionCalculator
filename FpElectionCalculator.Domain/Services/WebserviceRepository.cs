using FpElectionCalculator.Domain.JsonModels;
using System.Collections.Generic;
using System.Linq;

namespace FpElectionCalculator.Domain.Services
{
    public class WebserviceRepository
    {
        public static bool IsPeselDisallowedToVote(string pesel)
        {
            string json = new WebserviceRawCommunication().GetPeopleDisallowedToVote();
            IList<Person> disallowedPersonList = new WebserviceDataParser().GetDisallowedPeopleList(json).PersonList;
            return disallowedPersonList.Any(p => p.Pesel == pesel);
        }

        public static CandidateList GetCandidatesList()
        {
            return new WebserviceDataParser().GetCandidatesList(new WebserviceRawCommunication().GetCandidates());
        }
    }
}
