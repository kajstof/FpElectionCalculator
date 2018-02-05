using System.Collections.Generic;
using FpElectionCalculator.Domain.DbModels;

namespace FpElectionCalculator.Domain.Services
{
    public class DatabaseAndWebserviceLogic
    {
        private readonly ElectionDbContext _context;

        public DatabaseAndWebserviceLogic(ElectionDbContext context)
        {
            _context = context;
        }

        public void InitializeDbWithCandidatesAndParties()
        {
            JsonModels.CandidatesList candidatesList = WebserviceLogic.GetCandidatesList();
            IList<DbModels.Party> partiesDb = candidatesList.ConvertToDbModel();
            new DatabaseInitializer(_context).InitializeDbWithCandidatesAndParties(partiesDb);
        }
    }
}