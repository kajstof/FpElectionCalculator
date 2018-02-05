using System.Collections.Generic;
using System.Linq;

namespace FpElectionCalculator.Domain.Services
{
    public class GetDbCandidatesService : IGetDbCandidatesService
    {
        private readonly DbModels.ElectionDbContext _context;

        public GetDbCandidatesService(DbModels.ElectionDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DbModels.Candidate> GetCandidates()
        {
            using (_context)
            {
                return _context.Candidates.ToList();
            }
        }
    }
}