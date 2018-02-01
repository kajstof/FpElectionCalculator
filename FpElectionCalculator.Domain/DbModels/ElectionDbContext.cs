using Microsoft.EntityFrameworkCore;

namespace FpElectionCalculator.Domain.DbModels
{
    public class ElectionDbContext : DbContext
    {
        private string _connectionString;
        public DbSet<User> Users { get; set; }

        //public ElectionDbContext(string connectionString, DbContextOptions<ElectionDbContext> options) : base(options)
        //{
        //    _connectionString = connectionString;
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string connectionString = @"Server=tcp:fp-electioncalculator-db.database.windows.net,1433;Initial Catalog=fp-electioncalculator-db;Persist Security Info=False;User ID=ksf;Password=FPElectionCalculator1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=fp-electioncalculator-db;Trusted_Connection=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
