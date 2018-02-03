using System.Collections.Generic;
using System.Linq;

namespace FpElectionCalculator.Domain.Services
{
    public static class JsonModelToDbModelConverter
    {
        public static IList<DbModels.Party> ConvertPartiesAndCandidatesFromJsonModelToDbModel(JsonModels.CandidatesList candidatesList)
        {
            IEnumerable<string> partiesJson = (from candidate in candidatesList.Candidates
                              select candidate.Party).Distinct();
            IList<DbModels.Party> partiesDb = new List<DbModels.Party>();
            foreach (var party in partiesJson)
            {
                var candidatesInParty = (from candidate in candidatesList.Candidates
                                         where candidate.Party == party
                                         select new DbModels.Candidate() { Name = candidate.Name }).ToList();
                partiesDb.Add(new DbModels.Party() { Name = party, Candidates = candidatesInParty });
            }
            return partiesDb;
        }
    }
}
