namespace FpElectionCalculator.Domain.DbModels
{
    public class Vote
    {
        public int VoteId { get; set; }
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        public int PartyId { get; set; }
        public Party Party { get; set; }
    }
}