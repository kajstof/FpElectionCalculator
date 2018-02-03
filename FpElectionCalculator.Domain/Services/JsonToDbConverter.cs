using FpElectionCalculator.Domain.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FpElectionCalculator.Domain.Services
{
    public class JsonToDbConverter
    {
        public IList<DbModels.Party> ConvertPartiesAndCandidatesFromJsonModelToDbModel(CandidatesList candidatesList)
        {
            var partiesJson = from candidate in candidatesList.Candidates
                              select candidate.Party.Distinct();
            IList<DbModels.Party> partiesDb = new List<DbModels.Party>();
            foreach (var party in partiesJson)
            {
                partiesDb.Add(new DbModels.Party() { Name = "" });
            }
            return partiesDb;
        }
    }
}
