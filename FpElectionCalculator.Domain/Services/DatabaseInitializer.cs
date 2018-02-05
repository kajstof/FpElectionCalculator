using System.Collections.Generic;
using System.Linq;

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

        public void InitializeDbWithCandidatesAndParties()
        {
            // Get CandidateList from service
            JsonModels.CandidateList candidateList = _service.GetCandidateList();
            // Convert JsonModel to DbModel
            ICollection<DbModels.Party> partiesDb = candidateList.ConvertToDbModel();

            using (_context)
            {
                // Check database and records exists
                bool databaseNotExists = _context.Database.EnsureCreated();
                if (!databaseNotExists && !_context.Candidates.Any())
                {
                    _context.AddRange(partiesDb);
                    _context.SaveChanges();
                }
            }

        }
    }
}