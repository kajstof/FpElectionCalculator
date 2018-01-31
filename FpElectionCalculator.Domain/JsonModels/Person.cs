using Newtonsoft.Json;

namespace FpElectionCalculator.Domain.JsonModels
{
    public class Person
    {
        [JsonProperty("pesel")]
        public ulong Pesel { get; set; }
    }
}
