using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FpElectionCalculator.Domain.DbModels
{
    [Table("Votes")]
    public class Vote
    {
        public int VoteId { get; set; }
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        public int PartyId { get; set; }
        public Party Party { get; set; }

        public IList<User> Users { get; set; }
    }
}