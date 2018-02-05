using System.Collections.Generic;

namespace FpElectionCalculator.Domain.Services
{
    public interface IGetDbCandidatesService
    {
        IEnumerable<DbModels.Candidate> GetCandidates();
    }
}