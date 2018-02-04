using System.ComponentModel.DataAnnotations.Schema;

namespace FpElectionCalculator.Domain.DbModels
{
    [Table("Votes")]
    public class Vote
    {
        public int VoteId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int? CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        //public int PartyID { get; set; }
        //public Party Party { get; set; }
    }
}