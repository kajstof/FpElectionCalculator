using System;
using System.Collections.Generic;
using System.Linq;
using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Services;

namespace FpElectionCalculator.Domain.Models
{
    public class User : DbModels.User
    {
        private LoginCredentials _loginCredentials;
        private new Pesel Pesel { get; }
        private readonly ElectionDbContext _context;
        private readonly IUserIsDisallowedToVoteService _userIsDisallowdToVoteService;
        private readonly IUserAlreadyVotedService _userAlreadyVotedService;

        public User(LoginCredentials loginCredentials, ElectionDbContext context, IUserIsDisallowedToVoteService userIsDisallowdToVoteService, IUserAlreadyVotedService userAlreadyVotedService)
        {
            _loginCredentials = loginCredentials;
            FirstName = loginCredentials.FirstName;
            LastName = loginCredentials.LastName;
            base.Pesel = loginCredentials.Pesel;
            Pesel = new Pesel(loginCredentials.Pesel);
            _context = context;
            _userIsDisallowdToVoteService = userIsDisallowdToVoteService;
            _userAlreadyVotedService = userAlreadyVotedService;
        }

        public LoginValidation Login()
        {
            LoginValidation loginValidation = new LoginValidation(FirstName, LastName, Pesel, _userIsDisallowdToVoteService, _userAlreadyVotedService);
            if (!loginValidation.Error)
                CreateUserIfNotExists();
            return loginValidation;
        }

        public bool Logout() => throw new NotImplementedException();
        public bool IsPeselDisallowedToVote() => WebserviceLogic.IsPeselDisallowedToVote(Pesel.ToString());

        public bool IsUserVoted()
        {
            bool voted = false;
            using (_context)
            {
                // Find user in database
                bool UserFunc(DbModels.User u) =>
                    u.FirstName.Equals(FirstName)
                    && u.LastName.Equals(LastName)
                    && u.Pesel.Equals(Pesel);

                bool userExists = _context.Users.Any(UserFunc);
                if (userExists)
                    voted = _context.Users.First(UserFunc).Votes.Any();
            }

            return voted;
        }

        public void CreateUserIfNotExists()
        {
            using (_context)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Find user in database
                        bool UserFunc(DbModels.User u) =>
                            u.FirstName.Equals(FirstName)
                            && u.LastName.Equals(LastName)
                            && u.Pesel.Equals(Pesel);

                        bool userExists = _context.Users.Any(UserFunc);
                        if (!userExists)
                        {
                            _context.Users.Add(new DbModels.User()
                            {
                                FirstName = FirstName,
                                LastName = LastName,
                                Pesel = base.Pesel
                            });
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                        }
                    }
                    catch (Exception) { }
                }
            }
        }

        public bool DoVote(IEnumerable<Candidate> candidateList)
        {
            bool voted = false;
            using (_context)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Find user in database
                        bool UserFunc(DbModels.User u) =>
                            u.FirstName.Equals(FirstName)
                            && u.LastName.Equals(LastName)
                            && u.Pesel.Equals(base.Pesel);

                        bool userExists = _context.Users.Any(UserFunc);
                        if (userExists)
                        {
                            DbModels.User user = _context.Users.Single(UserFunc);
                            voted = user.Votes.Any();

                            bool isPeselDisallowedToVote =
                                WebserviceLogic.IsPeselDisallowedToVote(base.Pesel);

                            if (!voted && !isPeselDisallowedToVote)
                            {
                                foreach (var candidate in candidateList)
                                {
                                    _context.Votes.Add(new Vote() { User = user, Candidate = candidate });
                                }
                            }

                            ;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            return voted;
        }
    }
}