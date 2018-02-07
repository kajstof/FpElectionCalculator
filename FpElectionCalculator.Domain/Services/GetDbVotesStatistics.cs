using System.Collections.Generic;
using System.Linq;
using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Models;

namespace FpElectionCalculator.Domain.Services
{
    public class GetDbVotesStatistics
    {
        private readonly ElectionDbContext _context;

        public GetDbVotesStatistics(ElectionDbContext context)
        {
            _context = context;
        }

        public int GetVotesCount() => _context.Users.Count(u => u.Voted);

        public (int, double) GetValidVotesCount()
        {
            int totalVotes = GetVotesCount();
            if (totalVotes == 0)
                return (0, 0);
            int validVotes = _context.Votes.GroupBy(n => n.UserId).Count(n => n.Count() == 1);
            double votesPercent = (double) validVotes / totalVotes * 100;
            return (validVotes, votesPercent);
        }

        public (int, double) GetInvalidVotesCount()
        {
            (int validVotes, double _) = GetValidVotesCount();
            int totalVotes = GetVotesCount();
            if (totalVotes == 0)
                return (0, 0);
            int invalidVotes = GetVotesCount() - validVotes;
            double votesPercent = (double) invalidVotes / totalVotes * 100;
            return (invalidVotes, votesPercent);
        }

        public List<CandidateResult> GetVotesResults()
        {
            var votesCount = GetVotesCount();
            List<CandidateResult> candidates = _context.Votes.GroupBy(n => n.UserId) // Grouped Votes by UserId
                .Where(n => n.Count() == 1) // Filter only where is exactly 1 vote (valid votes)
                .Select(votes => votes.Single().Candidate) // Select Candidate (one level deeper)
                .GroupBy(c => c.CandidateId) // Group the same candidates
                .Select(c => new CandidateResult // Create new type with results
                {
                    CandidateName = c.First().Name,
                    PartyName = c.First().Party.Name,
                    VotesCount = c.Count(),
                    VotesPercent = ((double) c.Count() / votesCount) * 100.0
                }).OrderByDescending(r => r.VotesCount).ToList();

            return candidates;
        }
    }
}