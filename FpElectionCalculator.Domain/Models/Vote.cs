namespace FpElectionCalculator.Domain.Models
{
    public class Vote
    {
        public Candidate Candidate { get; set; }
        public Party Party { get; set; }
    }
}