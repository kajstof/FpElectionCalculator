using Newtonsoft.Json;

namespace FpElectionCalculator.Domain.JsonModels
{
    public class Person
    {
        [JsonProperty("pesel")]
        public string Pesel { get; set; }
    }
}
