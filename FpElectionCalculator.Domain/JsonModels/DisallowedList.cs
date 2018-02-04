using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FpElectionCalculator.Domain.JsonModels
{
    public class DisallowedList
    {
        [JsonProperty("publicationDate")]
        public DateTime PublicationDate { get; set; }
        [JsonProperty("person")]
        public IList<Person> PersonList { get; set; }
    }
}
