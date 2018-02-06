using FpElectionCalculator.Domain.JsonModels;
using System.Collections.Generic;
using System.Linq;
using FpElectionCalculator.Domain.Interfaces;

namespace FpElectionCalculator.Domain.Services
{
    public class CheckUserIsDisallowedToVoteService : ICheckUserIsDisallowedToVoteService
    {
        private readonly WebserviceRawCommunication _webservice;

        public CheckUserIsDisallowedToVoteService(WebserviceRawCommunication webservice)
        {
            _webservice = webservice;
        }

        public bool IsPeselDisallowedToVote(string pesel)
        {
            string json = new WebserviceRawCommunication().GetPeopleDisallowedToVote();
            ICollection<Person> disallowedPersonList = new WebserviceDataParser().GetDisallowedPeopleList(json).PersonList;
            return disallowedPersonList.Any(p => p.Pesel == pesel);
        }
    }
}
