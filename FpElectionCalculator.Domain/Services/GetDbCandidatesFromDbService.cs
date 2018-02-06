using System.Collections.Generic;
using System.Linq;
using FpElectionCalculator.Domain.Interfaces;

namespace FpElectionCalculator.Domain.Services
{
    public class GetDbCandidatesFromDbService : IGetDbCandidatesService
    {
        private readonly DbModels.ElectionDbContext _context;

        public GetDbCandidatesFromDbService(DbModels.ElectionDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DbModels.Candidate> GetCandidates()
        {
            return _context.Candidates.ToList();
        }
    }
}