using System;
using System.Collections.Generic;
using System.Text;

namespace FpElectionCalculator.Domain.Services
{
    public static class FirstRunDatabaseInitializer
    {
        public static void InitializeDbWithCandidatesAndParties()
        {
            JsonModels.CandidatesList candidatesList = WebserviceConnector.GetCandidatesList();
            IList<DbModels.Party> partiesDb = JsonModelToDbModelConverter.ConvertPartiesAndCandidatesFromJsonModelToDbModel(candidatesList);
            DatabaseConnector.InitializeDbWithCandidatesAndParties(partiesDb);
        }
    }
}
