﻿using FpElectionCalculator.Domain.JsonModels;
using FpElectionCalculator.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FpElectionCalculator.Domain.Tests
{
    public class JsonToDbConverterTests
    {
        private readonly WebserviceDataParser _parser;

        public JsonToDbConverterTests()
        {
            _parser = new WebserviceDataParser();
        }

        private IList<DbModels.Party> Execute(string json)
        {
            CandidateList candidatesList = _parser.GetCandidatesList(json);
            return candidatesList.ConvertToDbModel().ToList();
        }

        [Fact]
        public void ReturnsIListPartyObject()
        {
            IList<DbModels.Party> parties = Execute(EmbeddedResources.GetResource("dbCandidates.json"));
            Assert.NotEmpty(parties);
            Assert.Equal(4, parties.Count);
            Assert.All(parties, p => Assert.True(p.Name.Length > 0));
            Assert.Equal(4, parties[0].Candidates.Count);
            Assert.Equal(3, parties[1].Candidates.Count);
            Assert.Equal(3, parties[2].Candidates.Count);
            Assert.Equal(3, parties[3].Candidates.Count);
            Assert.Equal("Mieszko I", parties[0].Candidates[0].Name);
            Assert.Equal("Kazimierz Wielki", parties[0].Candidates[3].Name);
            Assert.Equal("Wazowie", parties[3].Name);
        }
    }
}
