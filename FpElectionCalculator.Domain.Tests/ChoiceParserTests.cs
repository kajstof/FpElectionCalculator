using System;
using System.Linq;
using FpElectionCalculator.Domain.Utils;
using Xunit;

namespace FpElectionCalculator.Domain.Tests
{
    public class ChoiceParserTests
    {
        private int[] Execute(string txt, int count)
        {
            return ChoiceParser.Parse(txt, count);
        }

        [Theory]
        [InlineData("", 13)]
        [InlineData("5", 13)]
        [InlineData("1,5,10,5, 0 1 0, 13", 13)]
        public void ParserReturnValidArray(string txt, int count)
        {
            int[] response = Execute(txt, count);
            Assert.NotNull(response);
        }

        [Theory]
        [InlineData("20", 13)]
        [InlineData("1,5,10, 0, 13", 13)]
        [InlineData("1,5,10,xxx, 0 1 0,,, 13", 13)]
        public void ParserReturnNull(string txt, int count)
        {
            int[] response = Execute(txt, count);
            Assert.Null(response);
        }
    }
}