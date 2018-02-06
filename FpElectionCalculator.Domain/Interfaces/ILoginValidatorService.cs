using System.Collections.Generic;
using FpElectionCalculator.Domain.Models;

namespace FpElectionCalculator.Domain.Interfaces
{
    public interface ILoginValidatorService
    {
        LoginValidation LoginValidation { get; }
        bool Validate();
        bool ValidateErrors();
        bool ValidateWarnings();
    }
}