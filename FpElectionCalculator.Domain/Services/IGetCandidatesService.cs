using System.Collections.Generic;
using FpElectionCalculator.Domain.DbModels;

namespace FpElectionCalculator.Domain.Services
{
    public interface IGetCandidatesService
    {
        IEnumerable<Candidate> GetCandidates();
    }
}