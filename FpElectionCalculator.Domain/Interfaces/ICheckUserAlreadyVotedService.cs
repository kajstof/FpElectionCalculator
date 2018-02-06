using FpElectionCalculator.Domain.Models;

namespace FpElectionCalculator.Domain.Interfaces
{
    public interface ICheckUserAlreadyVotedService
    {
        bool IsUserVoted();
    }
}