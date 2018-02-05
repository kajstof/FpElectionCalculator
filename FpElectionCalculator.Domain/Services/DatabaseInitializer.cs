using System.Collections.Generic;
using System.Linq;
using FpElectionCalculator.Domain.DbModels;

namespace FpElectionCalculator.Domain.Services
{
    public class DatabaseInitializer
    {
        private readonly ElectionDbContext _context;
        public DatabaseInitializer(ElectionDbContext context)
        {
            _context = context;
        }

        public void InitializeDbWithCandidatesAndParties(IEnumerable<Party> partiesDb)
        {
            using (_context)
            {
                bool databaseNotExists = _context.Database.EnsureCreated();
                if (!databaseNotExists && !_context.Candidates.ToList().Any())
                {
                    _context.AddRange(partiesDb);
                    _context.SaveChanges();
                }
            }
        }
    }
}