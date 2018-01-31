using System.ComponentModel.DataAnnotations;

namespace FpElectionCalculator.Domain.DbModels
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public int PartyId { get; set; }
        public Party Party { get; set; }
    }
}