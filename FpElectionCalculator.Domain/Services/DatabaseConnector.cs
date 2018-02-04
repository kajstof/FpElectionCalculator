using FpElectionCalculator.Domain.DbModels;
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
                if (!databaseNotExists && context.Candidates.ToList().Any())
                {
                    context.AddRange(partiesDb);
                context.SaveChanges();
                }
            }
        }

        internal static bool IsUserVoted(string firstName, string lastName, string pesel)
        {
            bool voted = false;
            using (var context = new ElectionDbContext())
            {
                // Find user in database
                bool UserFunc(User u) => u.FirstName.Equals(firstName) && u.LastName.Equals(lastName) && u.Pesel.Equals(pesel);
                bool userExists = context.Users.Any(UserFunc);
                if (userExists)
                    voted = context.Users.First(UserFunc).Votes.Any();
            }

            return voted;
        }
    }
}
