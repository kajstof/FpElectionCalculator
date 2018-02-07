using FpElectionCalculator.Domain.DbModels;

namespace FpElectionCalculator.Domain.Models
{
    public class CandidateResult
    {
        public string CandidateName { get; set; }
        public string PartyName { get; set; }
        public int VotesCount { get; set; }
        public double VotesPercent { get; set; }
    }
}