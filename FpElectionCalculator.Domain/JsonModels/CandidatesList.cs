using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FpElectionCalculator.Domain.JsonModels
{
    public class CandidatesList
    {
        [JsonProperty("publicationDate")]
        public DateTime PublicationDate { get; set; }
        [JsonProperty("candidate")]
        public IList<Candidate> Candidates { get; set; }
    }
}
