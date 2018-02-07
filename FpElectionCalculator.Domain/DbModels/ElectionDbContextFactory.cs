using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FpElectionCalculator.Domain.DbModels
{
    public class ElectionDbContextFactory : IDesignTimeDbContextFactory<ElectionDbContext>
    {
        private static IConfigurationRoot Configuration { get; set; }

        public ElectionDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            return new ElectionDbContext(Configuration.GetConnectionString("FpElectionDatabase"));
        }
    }
}