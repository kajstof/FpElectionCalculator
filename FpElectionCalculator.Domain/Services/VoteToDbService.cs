using System;
using System.Collections.Generic;
using System.Linq;
using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Interfaces;
using FpElectionCalculator.Domain.Models;

namespace FpElectionCalculator.Domain.Services
{
    public class VoteToDbService : IVoteService
    {
        private readonly bool _logged;
        private readonly LoginCredentials _loginCredentials;
        private readonly ILoginValidatorService _loginValidatorService;
        private readonly ElectionDbContext _context;
        private readonly WebserviceRawCommunication _webservice;

        public VoteToDbService(bool logged, LoginCredentials loginCredentials,
            ILoginValidatorService loginValidatorService,
            ElectionDbContext context,
            WebserviceRawCommunication webservice)
        {
            _logged = logged;
            _loginCredentials = loginCredentials;
            _loginValidatorService = loginValidatorService;
            _context = context;
            _webservice = webservice;
        }

        public bool Vote(IEnumerable<Candidate> candidateList)
        {
            bool voted = false;

            // Is user logged
            if (_logged)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    // Prepare user predicate function
                    bool UserPredicate(DbModels.User u) =>
                        u.FirstName.Equals(_loginCredentials.FirstName)
                        && u.LastName.Equals(_loginCredentials.LastName)
                        && u.Pesel.Equals(_loginCredentials.Pesel);

                    // Check for exists user
                    if (_context.Users.Any(UserPredicate))
                    {
                        // Find voted
                        DbModels.User user = _context.Users.Single(UserPredicate);
                        bool validateWarnings = _loginValidatorService.ValidateWarnings();

                        if (!user.Voted && !validateWarnings)
                        {
                            foreach (var candidate in candidateList)
                            {
                                _context.Votes.Add(new Vote() {User = user, Candidate = candidate});
                            }

                            user.Voted = true;
                            _context.SaveChanges();
                        }
                    }
                }
            }

            return voted;
        }
    }
}