namespace FpElectionCalculator.Domain.Interfaces
{
    public interface ICheckUserIsDisallowedToVoteService
    {
        bool IsPeselDisallowedToVote(string pesel);
    }
}