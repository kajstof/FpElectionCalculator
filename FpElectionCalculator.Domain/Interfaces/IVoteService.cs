using System.Collections.Generic;
using FpElectionCalculator.Domain.DbModels;

namespace FpElectionCalculator.Domain.Interfaces
{
    interface IVoteService
    {
        bool Vote(IEnumerable<Candidate> candidateList);
    }
}