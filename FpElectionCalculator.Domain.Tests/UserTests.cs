using FpElectionCalculator.Domain.Models;
using Xunit;

namespace FpElectionCalculator.Domain.Tests
{
    public class UserLoginTests
    {
        // Attention!
        // Data used for these tests are depend from other sources (like webservice or database)
        // If eg.:
        // - pesel will shown on black list then tests can be failed!
        // - Some user did invalid vote (0 or 2, 3, ... candidates)

        private static LoginValidation Execute(string firstName, string lastName, string pesel)
        {
            User user = new User(new LoginCredentials(firstName, lastName, pesel));
            LoginValidation loginValidation = user.Login();
            return loginValidation;
        }

        [Theory]
        [InlineData("Jan", "Nowak", "86030218897")]
        [InlineData("Adam", "Adamski", "86110218898")]
        [InlineData("Ed", "Mu", "00210212891")]
        public void UserCanVote(string firstName, string lastName, string pesel)
        {
            LoginValidation loginValidation = Execute(firstName, lastName, pesel);
            Assert.True(loginValidation.Error);
        }

        [Theory]
        [InlineData("J", "Nowak", "86030218897")]
        [InlineData("Adam", "A", "86110218898")]
        [InlineData("Gu", "He", "0021021289")]
        [InlineData("Li", "Fe", "0031021284")]
        public void UserCantVote(string firstName, string lastName, string pesel)
        {
            LoginValidation loginValidation = Execute(firstName, lastName, pesel);
            Assert.False(loginValidation.Error);
        }

        [Fact]
        public void ReturnsLoginValidatorErrors()
        {
            LoginValidation loginValidation = Execute("A", "B", "0");
            Assert.False(loginValidation.Error);
            Assert.Contains(loginValidation.LoginErrors, e => e == LoginError.UserFirstNameIsTooShort);
            Assert.Contains(loginValidation.LoginErrors, e => e == LoginError.UserLastNameIsTooShort);
            Assert.Contains(loginValidation.LoginErrors, e => e == LoginError.PeselIsNotValid);

            loginValidation = Execute("Ce", "De", "10321519761");
            Assert.False(loginValidation.Warning);
            Assert.Contains(loginValidation.LoginWarnings, e => e == LoginWarning.UserIsNotEighteen);
        }
    }
}
