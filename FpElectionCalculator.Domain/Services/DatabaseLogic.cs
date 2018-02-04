using System;
using FpElectionCalculator.Domain.DbModels;
using System.Collections.Generic;
using System.Linq;
using FpElectionCalculator.Domain.Models;

namespace FpElectionCalculator.Domain.Services
{
    internal static class DatabaseLogic
    {
        internal static void InitializeDbWithCandidatesAndParties(IEnumerable<Party> partiesDb)
        {
            using (var context = new ElectionDbContext())
            {
                bool databaseNotExists = context.Database.EnsureCreated();
                if (!databaseNotExists && !context.Candidates.ToList().Any())
                {
                    context.AddRange(partiesDb);
                    context.SaveChanges();
                }
            }
        }

        internal static bool IsUserVoted(LoginCredentials loginCredentials)
        {
            bool voted = false;
            using (var context = new ElectionDbContext())
            {
                // Find user in database
                bool UserFunc(DbModels.User u) =>
                    u.FirstName.Equals(loginCredentials.FirstName)
                    && u.LastName.Equals(loginCredentials.LastName)
                    && u.Pesel.Equals(loginCredentials.Pesel);

                bool userExists = context.Users.Any(UserFunc);
                if (userExists)
                    voted = context.Users.First(UserFunc).Votes.Any();
            }

            return voted;
        }

        internal static IEnumerable<Candidate> GetCandidates()
        {
            using (var context = new ElectionDbContext())
            {
                return context.Candidates.ToList();
            }
        }

        public static void CreateUserIfNotExists(LoginCredentials loginCredentials)
        {
            using (var context = new ElectionDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // Find user in database
                        bool UserFunc(DbModels.User u) =>
                            u.FirstName.Equals(loginCredentials.FirstName)
                            && u.LastName.Equals(loginCredentials.LastName)
                            && u.Pesel.Equals(loginCredentials.Pesel);

                        bool userExists = context.Users.Any(UserFunc);
                        if (!userExists)
                        {
                            context.Users.Add(new DbModels.User()
                            {
                                FirstName = loginCredentials.FirstName,
                                LastName = loginCredentials.LastName,
                                Pesel = loginCredentials.Pesel
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
    }
}