namespace FpElectionCalculator.Domain.Services
{
    public interface IUserIsDisallowedToVoteService
    {
        bool IsPeselDisallowedToVote(string pesel);
    }
}