using FpElectionCalculator.Domain.JsonModels;
using Newtonsoft.Json.Linq;

namespace FpElectionCalculator.Domain
{
    public class WebserviceDataParser
    {
        public CandidatesList GetCandidatesList(string json)
        {
            return JObject.Parse(json).SelectToken("candidates").ToObject<CandidatesList>();
        }

        public DisallowedList GetDisallowedPeopleList(string json)
        {
            return JObject.Parse(json).SelectToken("disallowed").ToObject<DisallowedList>();
        }
    }
}
