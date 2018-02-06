using System;
using System.Linq;
using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Interfaces;
using FpElectionCalculator.Domain.Models;

namespace FpElectionCalculator.Domain.Services
{
    public class CreateUserInDbService : ICreateUserService
    {
        private readonly ElectionDbContext _context;

        public CreateUserInDbService(ElectionDbContext context)
        {
            _context = context;
        }

        public bool CreateUser(LoginCredentials loginCredentials)
        {
            bool userCreated = false;

            using (var transaction = _context.Database.BeginTransaction())
            {
                // Find user in database
                bool UserPredicate(DbModels.User u) =>
                    u.FirstName.Equals(loginCredentials.FirstName)
                    && u.LastName.Equals(loginCredentials.LastName)
                    && u.Pesel.Equals(loginCredentials.Pesel);

                // Check user exists
                if (!_context.Users.Any(UserPredicate))
                {
                    _context.Users.Add(new DbModels.User()
                    {
                        FirstName = loginCredentials.FirstName,
                        LastName = loginCredentials.LastName,
                        Pesel = loginCredentials.Pesel
                    });
                    transaction.Commit();
                    userCreated = true;
                }
                else
                {
                    transaction.Rollback();
                }
            }

            return userCreated;
        }
    }
}