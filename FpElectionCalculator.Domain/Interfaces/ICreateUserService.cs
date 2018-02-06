using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Models;

namespace FpElectionCalculator.Domain.Interfaces
{
    public interface ICreateUserService
    {
        bool CreateUser(LoginCredentials loginCredentials);
    }
}