using System;
using System.Collections.Generic;
using FpElectionCalculator.Domain.DbModels;

namespace FpElectionCalculator.Domain.Services
{
    public class GetDbVotesList
    {
        private readonly ElectionDbContext _context;

        public GetDbVotesList(ElectionDbContext context)
        {
            _context = context;
        }

        public string GetVoteStatistics()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}