namespace FpElectionCalculator.Domain.Services
{
    public interface IGetJsonCandidateListService
    {
        JsonModels.CandidateList GetCandidateList();
    }
}