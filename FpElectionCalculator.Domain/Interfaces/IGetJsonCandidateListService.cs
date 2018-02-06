namespace FpElectionCalculator.Domain.Interfaces
{
    public interface IGetJsonCandidateListService
    {
        JsonModels.CandidateList GetCandidateList();
    }
}