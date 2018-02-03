using System.Collections.Generic;

namespace FpElectionCalculator.Domain.Models
{
    public class Party
    {
        public string Name { get; set; }
        public IList<Candidate> Candidates { get; set; }
    }
}