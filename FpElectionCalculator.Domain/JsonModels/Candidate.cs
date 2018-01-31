using Newtonsoft.Json;

namespace FpElectionCalculator.Domain.JsonModels
{
    public class Candidate
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("party")]
        public string Party { get; set; }
    }
}