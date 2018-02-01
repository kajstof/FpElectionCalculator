using FpElectionCalculator.Domain.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FpElectionCalculator.Domain
{
    public static class DbInitializer
    {
        public static void Initialize(ElectionDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any candidates
            //    if (context.Candidates.Any())
            //    {
            //        return;
            //    }

            //    var c
        }
    }
}
