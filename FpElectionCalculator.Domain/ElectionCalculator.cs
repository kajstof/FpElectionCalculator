using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Services;
using System;
using System.Collections.Generic;

namespace FpElectionCalculator.Domain
{
    public static class ElectionCalculator
    {
        public static void Run()
        {

            using (var context = new ElectionDbContext())
            {
                context.Database.EnsureDeleted();
            }

            FirstRunDatabaseInitializer.InitializeDbWithCandidatesAndParties();
        }

        private static void GetCandidatesListFromWebservice() => throw new NotImplementedException();
        private static void PutCandidatesListToDb() => throw new NotImplementedException();
        private static void GetCandidatesListFromDb() => throw new NotImplementedException();
        private static IList<Models.Candidate> GetCandidatesFromWebservice() => throw new NotImplementedException();
        private static void GeneratePdfFile() => throw new NotImplementedException();
        private static void GenerateCsvFile() => throw new NotImplementedException();
        private static void GenerateActualReport() => throw new NotImplementedException();
    }
}