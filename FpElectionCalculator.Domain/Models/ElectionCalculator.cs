using System;
using System.Collections.Generic;
using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Services;

namespace FpElectionCalculator.Domain.Models
{
    public class ElectionCalculator
    {
        private readonly ElectionDbContext _context;

        public ElectionCalculator(ElectionDbContext context)
        {
            _context = context;
        }

        public void Run()
        {
            using (_context)
            {
                _context.Database.EnsureDeleted();
            }
        }

        private void GeneratePdfFile() => throw new NotImplementedException();
        private void GenerateCsvFile() => throw new NotImplementedException();
        private void GenerateActualReport() => throw new NotImplementedException();
    }
}