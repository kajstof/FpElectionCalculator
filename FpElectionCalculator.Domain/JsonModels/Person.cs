using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FpElectionCalculator.Domain.JsonModels
{
    public class Person
    {
        [JsonProperty("pesel")]
        public ulong Pesel { get; set; }
    }
}
