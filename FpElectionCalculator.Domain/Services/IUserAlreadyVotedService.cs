using FpElectionCalculator.Domain.Models;

namespace FpElectionCalculator.Domain.Services
{
    public interface IUserAlreadyVotedService
    {
        bool IsUserVoted(LoginCredentials loginCredentials);
    }
}