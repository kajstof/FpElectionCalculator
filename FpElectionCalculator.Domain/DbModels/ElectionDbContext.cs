using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FpElectionCalculator.Domain.DbModels
{
    class ElectionDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Server=tcp:fp-electioncalculator-db.database.windows.net,1433;Initial Catalog=fp-electioncalculator-db;Persist Security Info=False;User ID=ksf;Password=FPElectionCalculator1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
