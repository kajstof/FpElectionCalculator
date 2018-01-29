using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FpElectionCalculator.Library
{
    public class CandidatesList
    {
        [JsonProperty("publicationDate")]
        public DateTime PublicationDate { get; set; }
        [JsonProperty("candidate")]
        public List<Candidate> Candidates { get; set; }
    }
}
