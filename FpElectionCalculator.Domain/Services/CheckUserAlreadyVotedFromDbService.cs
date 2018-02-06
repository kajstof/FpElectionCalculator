using System.Linq;
using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Interfaces;
using FpElectionCalculator.Domain.Models;

namespace FpElectionCalculator.Domain.Services
{
    public class CheckUserAlreadyVotedFromDbService : ICheckUserAlreadyVotedService
    {
        private readonly LoginCredentials _loginCredentials;
        private readonly ElectionDbContext _context;

        public CheckUserAlreadyVotedFromDbService(LoginCredentials loginCredentials, ElectionDbContext context)
        {
            _loginCredentials = loginCredentials;
            _context = context;
        }

        public bool IsUserVoted()
        {
            // Prepare user predicate function
            bool UserPredicate(DbModels.User u) =>
                u.FirstName.Equals(_loginCredentials.FirstName)
                && u.LastName.Equals(_loginCredentials.LastName)
                && u.Pesel.Equals(_loginCredentials.Pesel)
                && u.Voted
                && u.Votes.Count == 0;

            // Check for exists user who voted
            return _context.Users.Any(UserPredicate);
        }
    }
}