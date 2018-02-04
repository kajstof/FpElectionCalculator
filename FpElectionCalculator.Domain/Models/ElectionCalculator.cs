﻿using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Services;
using System;
using System.Collections.Generic;

namespace FpElectionCalculator.Domain.Models
{
    public static class ElectionCalculator
    {
        public static void Run()
        {
            //using (var context = new ElectionDbContext())
            //{
            //    context.Database.EnsureDeleted();
            //}

            DatabaseAndWebserviceLogic.InitializeDbWithCandidatesAndParties();
        }

        public static IEnumerable<Candidate> GetCandidates() => DatabaseLogic.GetCandidates();
        private static void GeneratePdfFile() => throw new NotImplementedException();
        private static void GenerateCsvFile() => throw new NotImplementedException();
        private static void GenerateActualReport() => throw new NotImplementedException();
    }
}