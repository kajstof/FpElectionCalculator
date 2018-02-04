using FpElectionCalculator.Domain.JsonModels;
using Newtonsoft.Json.Linq;

namespace FpElectionCalculator.Domain.Services
{
    public static class WebserviceDataParser
    {
        public static CandidatesList GetCandidatesList(string json)
        {
            return JObject.Parse(json).SelectToken("candidates").ToObject<CandidatesList>();
        }

        public static DisallowedList GetDisallowedPeopleList(string json)
        {
            return JObject.Parse(json).SelectToken("disallowed").ToObject<DisallowedList>();
        }
    }
}
