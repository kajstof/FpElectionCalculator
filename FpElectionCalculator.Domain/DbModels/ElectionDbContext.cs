using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FpElectionCalculator.Domain.DbModels
{
    public class ElectionDbContext : DbContext
    {
        // LocalDB
        string _connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=fp-electioncalculator-db;Trusted_Connection=True;MultipleActiveResultSets=true";
        // Azure
        //string _connectionString = @"Server=tcp:fp-electioncalculator-db.database.windows.net,1433;Initial Catalog=fp-electioncalculator-db;Persist Security Info=False;User ID=ksf;Password=FPElectionCalculator1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
