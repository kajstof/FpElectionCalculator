using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FpElectionCalculator.Library
{
    public class Person
    {
        [JsonProperty("pesel")]
        public ulong Pesel { get; set; }
    }
}
