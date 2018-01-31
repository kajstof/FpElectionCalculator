using FpElectionCalculator.Domain;
using System;
using System.Linq;
using Xunit;

namespace FpElectionCalculator.Tests
{
    public class PeselValidationTests
    {
        [Theory]
        [InlineData(99010112342)]
        public void ValidPeselReturnsTrue(ulong pesel)
        {
            Assert.True(new PeselValidator(pesel.ToString()).IsValid());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(12121278901234)]
        [InlineData(12340178901)]
        [InlineData(67109945623)]
        public void InvalidPeselReturnsFalse(ulong pesel)
        {
            Assert.False(new PeselValidator(pesel.ToString()).IsValid());
        }
    }
}
