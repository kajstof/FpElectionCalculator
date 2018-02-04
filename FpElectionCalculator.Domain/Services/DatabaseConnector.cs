using FpElectionCalculator.Domain.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FpElectionCalculator.Domain.Services
{
    public static class DatabaseConnector
    {
        internal static void InitializeDbWithCandidatesAndParties(IList<Party> partiesDb)
        {
            using (var context = new ElectionDbContext())
            {
                bool databaseNotExists = context.Database.EnsureCreated();
                if (databaseNotExists || (!databaseNotExists && context.Candidates.ToList().Count() == 0))
                    context.AddRange(partiesDb);
                context.SaveChanges();
            }
        }

        internal static bool IsUserVoted(string firstName, string lastName, string pesel)
        {
            bool voted = false;
            using (var context = new ElectionDbContext())
            {
                // Find user in database
                Func<User, bool> func = u => u.FirstName.Equals(firstName) && u.LastName.Equals(lastName) && u.Pesel.Equals(pesel);
                bool userExists = context.Users.Any(func);
                if (userExists)
                    voted = context.Users.First(func).Votes.Count() > 0;
            }

            return voted;
        }
    }
}
