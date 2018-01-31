using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FpElectionCalculator.Domain.JsonModels
{
    public class DisallowedPeopleList
    {
        [JsonProperty("publicationDate")]
        public DateTime PublicationDate { get; set; }
        [JsonProperty("person")]
        public IList<DisallowedPerson> DisallowedPeople { get; set; }
    }
}
