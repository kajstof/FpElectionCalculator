using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FpElectionCalculator.Domain.DbModels
{
    public class ElectionDbContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public ElectionDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
    }
}
