using System;
using System.Collections.Generic;
using System.Linq;
using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Models;

namespace FpElectionCalculator.Domain.Services
{
    public static class DatabaseAndWebserviceLogic
    {
        public static IList<DbModels.Party> ConvertPartiesAndCandidatesFromJsonModelToDbModel(
            JsonModels.CandidatesList candidatesList)
        {
            IEnumerable<string> partiesJson = (from candidate in candidatesList.CandidateList
                select candidate.Party).Distinct();
            IList<DbModels.Party> partiesDb = new List<DbModels.Party>();
            foreach (var party in partiesJson)
            {
                var candidatesInParty = (from candidate in candidatesList.CandidateList
                    where candidate.Party == party
                    select new DbModels.Candidate() {Name = candidate.Name}).ToList();
                partiesDb.Add(new DbModels.Party() {Name = party, Candidates = candidatesInParty});
            }

            return partiesDb;
        }

        public static void InitializeDbWithCandidatesAndParties()
        {
            JsonModels.CandidatesList candidatesList = WebserviceLogic.GetCandidatesList();
            IList<DbModels.Party> partiesDb = ConvertPartiesAndCandidatesFromJsonModelToDbModel(candidatesList);
            DatabaseLogic.InitializeDbWithCandidatesAndParties(partiesDb);
        }

        internal static bool DoVote(LoginCredentials loginCredentials, IEnumerable<Candidate> candidateList)
        {
            bool voted = false;
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
                        if (userExists)
                        {
                            DbModels.User user = context.Users.Single(UserFunc);
                            voted = user.Votes.Any();

                            bool isPeselDisallowedToVote =
                                WebserviceLogic.IsPeselDisallowedToVote(loginCredentials.Pesel);

                            if (!voted && !isPeselDisallowedToVote)
                            {
                                foreach (var candidate in candidateList)
                                {
                                    context.Votes.Add(new Vote() {User = user, Candidate = candidate});
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