using System;
using System.Collections.Generic;
using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Services;

namespace FpElectionCalculator.Domain.Services
{
    public class RaportGeneratorService
    {
        private readonly ElectionDbContext _context;

        public RaportGeneratorService(ElectionDbContext context) => _context = context;

        public void GeneratePdfFile() => throw new NotImplementedException();
        public void GenerateCsvFile() => throw new NotImplementedException();
        public void GenerateActualReport() => throw new NotImplementedException();
    }
}