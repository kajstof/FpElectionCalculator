using FpElectionCalculator.Domain.Models;
using Xunit;

namespace FpElectionCalculator.Domain.Tests
{
    public class UserLoginTests
    {
        private static LoginValidator Execute(string firstName, string lastName, string pesel)
        {
            User user = new User(firstName, lastName, pesel);
            LoginValidator loginValidator = user.Login();
            return loginValidator;
        }

        [Theory]
        [InlineData("Jan", "Nowak", "86030218897")]
        [InlineData("Adam", "Adamski", "86110218898")]
        [InlineData("Ed", "Mu", "00210212891")]
        public void UserCanVote(string firstName, string lastName, string pesel)
        {
            LoginValidator loginValidator = Execute(firstName, lastName, pesel);
            Assert.True(loginValidator.Error);
        }

        [Theory]
        [InlineData("J", "Nowak", "86030218897")]
        [InlineData("Adam", "A", "86110218898")]
        [InlineData("Gu", "He", "0021021289")]
        [InlineData("Li", "Fe", "0031021284")]
        public void UserCantVote(string firstName, string lastName, string pesel)
        {
            LoginValidator loginValidator = Execute(firstName, lastName, pesel);
            Assert.False(loginValidator.Error);
        }

        [Fact]
        public void ReturnsLoginValidatorErrors()
        {
            LoginValidator loginValidator = Execute("A", "B", "0");
            Assert.False(loginValidator.Error);
            Assert.Contains(loginValidator.LoginErrors, e => e == LoginError.UserFirstNameIsTooShort);
            Assert.Contains(loginValidator.LoginErrors, e => e == LoginError.UserLastNameIsTooShort);
            Assert.Contains(loginValidator.LoginErrors, e => e == LoginError.PeselIsNotValid);

            loginValidator = Execute("Ce", "De", "10321519761");
            Assert.False(loginValidator.Error);
            Assert.Contains(loginValidator.LoginErrors, e => e == LoginError.UserIsNotEighteen);
        }
    }
}
