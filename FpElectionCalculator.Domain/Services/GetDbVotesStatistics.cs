using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FpElectionCalculator.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

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
        public int GetValidVotesCount() => _context.Votes.GroupBy(n => n.UserId).Count(n => n.Count() == 1);
        public int GetInvalidVotesCount() => GetVotesCount() - GetValidVotesCount();

        public ICollection<(string candidate, string party, int votes, double votesPercent)> GetVotesResults()
        {
            IQueryable<IGrouping<int, Candidate>> xxx = _context.Votes.GroupBy(n => n.UserId).Where(n => n.Count() == 1)
                .Select(votes => votes.First().Candidate).GroupBy(c => c.CandidateId);

            ICollection<(string candidate, string party, int votes, double votesPercent)> response =
                new List<(string candidate, string party, int votes, double votesPercent)>();
            foreach (var x in xxx)
            {
            }

            // TODO
            throw new NotImplementedException();
        }
    }
}