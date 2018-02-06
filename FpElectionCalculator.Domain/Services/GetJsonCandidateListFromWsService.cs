using FpElectionCalculator.Domain.Interfaces;
using FpElectionCalculator.Domain.JsonModels;

namespace FpElectionCalculator.Domain.Services
{
    public class GetJsonCandidateListFromWsService : IGetJsonCandidateListService
    {
        private readonly WebserviceRawCommunication _webservice;

        public GetJsonCandidateListFromWsService(WebserviceRawCommunication webservice)
        {
            _webservice = webservice;
        }

        public CandidateList GetCandidateList()
        {
            return new WebserviceDataParser().GetCandidatesList(_webservice.GetCandidates());
        }
    }
}