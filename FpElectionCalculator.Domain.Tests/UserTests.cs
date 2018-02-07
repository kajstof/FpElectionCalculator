using System.IO;
using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Models;
using FpElectionCalculator.Domain.Services;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace FpElectionCalculator.Domain.Tests
{
    public class DatabaseAndWebserviceFixture
    {
        public ElectionDbContext Context { get; }
        public WebserviceRawCommunication Webservice { get; }
        public static IConfigurationRoot Configuration { get; private set; }

        public DatabaseAndWebserviceFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            Context = new ElectionDbContext(Configuration.GetConnectionString("FpElectionDatabase"));
            Webservice = new WebserviceRawCommunication();

            // Try to create database (if not exists) and populate with candidates data
            var dbInitializer = new DatabaseInitializer(Context, new GetJsonCandidateListFromWsService(Webservice));
            dbInitializer.DeleteTablesInDatabase();
            dbInitializer.InitializeDbWithCandidatesAndParties();
        }
    }

    public class UserLoginTests : IClassFixture<DatabaseAndWebserviceFixture>
    {
        private DatabaseAndWebserviceFixture _fixture;

        public UserLoginTests(DatabaseAndWebserviceFixture fixture)
        {
            _fixture = fixture;
        }

        // Attention!
        // Data used for these tests are depend from other sources (like webservice or database)
        // If eg.:
        // - pesel will shown on black list then tests can be failed!
        // - Some user did invalid vote (0 or 2, 3, ... candidates)

        private LoginValidation Execute(string firstName, string lastName, string pesel)
        {
            LoginCredentials loginCredentials = new LoginCredentials(firstName, lastName, pesel);
            Domain.Models.User user = new Domain.Models.User(loginCredentials, _fixture.Context, _fixture.Webservice);
            LoginValidation loginValidation = user.Login();
            return loginValidation;
        }

        [Trait("Category", "notcircleci")]
        [Theory]
        [InlineData("Jan", "Nowak", "86030218897")]
        [InlineData("Adam", "Adamski", "86110218898")]
        [InlineData("Ed", "Mu", "00210212891")]
        public void UserCanVote(string firstName, string lastName, string pesel)
        {
            LoginValidation loginValidation = Execute(firstName, lastName, pesel);
            Assert.False(loginValidation.Error && loginValidation.Warning);
        }

        [Trait("Category", "notcircleci")]
        [Theory]
        [InlineData("J", "Nowak", "86030218897")]
        [InlineData("Adam", "A", "86110218898")]
        [InlineData("Gu", "He", "0021021289")]
        [InlineData("Li", "Fe", "0031021284")]
        public void UserCantVote(string firstName, string lastName, string pesel)
        {
            LoginValidation loginValidation = Execute(firstName, lastName, pesel);
            Assert.True(loginValidation.Error || loginValidation.Warning);
        }

        [Trait("Category", "notcircleci")]
        [Fact]
        public void ReturnsLoginValidatorErrors()
        {
            LoginValidation loginValidation = Execute("A", "B", "0");
            Assert.True(loginValidation.Error);
            Assert.Contains(loginValidation.LoginErrors,
                e => e == LoginError.UserFirstNameIsTooShort);
            Assert.Contains(loginValidation.LoginErrors,
                e => e == LoginError.UserLastNameIsTooShort);
            Assert.Contains(loginValidation.LoginErrors,
                e => e == LoginError.PeselIsNotValid);

            loginValidation = Execute("Ce", "De", "10321519761");
            Assert.True(loginValidation.Warning);
            Assert.Contains(loginValidation.LoginWarnings,
                e => e == LoginWarning.UserIsNotEighteen);
        }
    }
}