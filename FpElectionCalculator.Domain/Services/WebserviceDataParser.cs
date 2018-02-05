using FpElectionCalculator.Domain.JsonModels;
using Newtonsoft.Json.Linq;

namespace FpElectionCalculator.Domain.Services
{
    public class WebserviceDataParser
    {
        public CandidateList GetCandidatesList(string json)
        {
            return JObject.Parse(json).SelectToken("candidates").ToObject<CandidateList>();
        }

        public DisallowedList GetDisallowedPeopleList(string json)
        {
            return JObject.Parse(json).SelectToken("disallowed").ToObject<DisallowedList>();
        }
    }
}