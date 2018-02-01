using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FpElectionCalculator.Domain.DbModels
{
    [Table("Votes")]
    public class Vote
    {
        public int VoteID { get; set; }
        //public int CandidateID { get; set; }
        //public Candidate Candidate { get; set; }
        //public int PartyID { get; set; }
        //public Party Party { get; set; }

        //public IList<User> Users { get; set; }
    }
}