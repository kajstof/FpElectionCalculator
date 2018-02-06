using System.Collections.Generic;

namespace FpElectionCalculator.Domain.Interfaces
{
    public interface IGetDbCandidatesService
    {
        IEnumerable<DbModels.Candidate> GetCandidates();
    }
}