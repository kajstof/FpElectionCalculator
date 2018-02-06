using System.Collections.Generic;
using System.Linq;
using FpElectionCalculator.Domain.Interfaces;

namespace FpElectionCalculator.Domain.Services
{
    public class DatabaseInitializer
    {
        private readonly DbModels.ElectionDbContext _context;
        private readonly IGetJsonCandidateListService _service;

        public DatabaseInitializer(DbModels.ElectionDbContext context, IGetJsonCandidateListService service)
        {
            _context = context;
            _service = service;
        }

        public void DeleteTablesInDatabase()
        {
            _context.Database.EnsureDeleted();
        }

        public void InitializeDbWithCandidatesAndParties()
        {
            // Check database and records exists
            bool databaseNotExists = _context.Database.EnsureCreated();
            if (databaseNotExists || !_context.Candidates.Any())
            {
                // Get CandidateList from service
                JsonModels.CandidateList candidateList = _service.GetCandidateList();
                // Convert JsonModel to DbModel
                ICollection<DbModels.Party> partyDbCollection = candidateList.ConvertToDbModel();
                // Add parties collection to database and save changes
                _context.AddRange(partyDbCollection);
                _context.SaveChanges();
            }
        }
    }
}