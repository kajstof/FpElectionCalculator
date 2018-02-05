using FpElectionCalculator.Domain.JsonModels;

namespace FpElectionCalculator.Domain.Services
{
    public class GetJsonCandidateListService : IGetJsonCandidateListService
    {
        private readonly WebserviceRawCommunication _webservice;

        public GetJsonCandidateListService(WebserviceRawCommunication webservice)
        {
            _webservice = webservice;
        }

        public CandidateList GetCandidateList()
        {
            return new WebserviceDataParser().GetCandidatesList(_webservice.GetCandidates());
        }
    }
}