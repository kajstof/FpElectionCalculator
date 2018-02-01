using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FpElectionCalculator.Domain.DbModels
{
    [Table("Candidates")]
    public class Candidate
    {
        public int CandidateID { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public int PartyID { get; set; }
        public Party Party { get; set; }
    }
}