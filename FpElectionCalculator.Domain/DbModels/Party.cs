using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FpElectionCalculator.Domain.DbModels
{
    [Table("Parties")]
    public class Party
    {
        public int PartyID { get; set; }
        public string Name { get; set; }

        public IList<Candidate> Candidates { get; set; }
    }
}