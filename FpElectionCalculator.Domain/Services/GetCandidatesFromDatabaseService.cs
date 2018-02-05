using System.Collections.Generic;
using System.Linq;
using FpElectionCalculator.Domain.DbModels;

namespace FpElectionCalculator.Domain.Services
{
    public class GetCandidatesFromDatabaseService : IGetCandidatesService
    {
        private readonly ElectionDbContext _context;

        public GetCandidatesFromDatabaseService(ElectionDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Candidate> GetCandidates()
        {
            using (_context)
            {
                return _context.Candidates.ToList();
            }
        }
    }
}