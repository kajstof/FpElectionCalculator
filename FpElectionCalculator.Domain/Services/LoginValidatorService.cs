using FpElectionCalculator.Domain.Interfaces;
using FpElectionCalculator.Domain.Models;

namespace FpElectionCalculator.Domain.Services
{
    public class LoginValidatorService : ILoginValidatorService
    {
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _pesel;
        private readonly Pesel _peselPesel;
        private readonly ICheckUserIsDisallowedToVoteService _serviceCheckUserIsDisallowedToVoteService;
        private readonly ICheckUserAlreadyVotedService _serviceCheckUserAlreadyVotedService;
        public LoginValidation LoginValidation { get; } = new LoginValidation();

        public LoginValidatorService(string firstName, string lastName, string pesel,
            ICheckUserIsDisallowedToVoteService serviceCheckUserIsDisallowedToVoteService,
            ICheckUserAlreadyVotedService serviceCheckUserAlreadyVotedService)
        {
            _firstName = firstName;
            _lastName = lastName;
            _pesel = pesel;
            _peselPesel = new Pesel(pesel);
            _serviceCheckUserIsDisallowedToVoteService = serviceCheckUserIsDisallowedToVoteService;
            _serviceCheckUserAlreadyVotedService = serviceCheckUserAlreadyVotedService;
        }

        public bool Validate()
        {
            ValidateWarnings();
            return ValidateErrors();
        }

        public bool ValidateErrors()
        {
            if (_pesel.Length != 11 || !_peselPesel.IsValid())
                LoginValidation.LoginErrors.Add(LoginError.PeselIsNotValid);

            if (_firstName.Length < 2)
                LoginValidation.LoginErrors.Add(LoginError.UserFirstNameIsTooShort);

            if (_lastName.Length < 2)
                LoginValidation.LoginErrors.Add(LoginError.UserLastNameIsTooShort);

            return LoginValidation.Error;
        }

        public bool ValidateWarnings()
        {
            if (_pesel.Length == 11 && _peselPesel.IsValid() && !_peselPesel.IsEighteen())
                LoginValidation.LoginWarnings.Add(LoginWarning.UserIsNotEighteen);

            if (_serviceCheckUserIsDisallowedToVoteService.IsPeselDisallowedToVote(_pesel))
                LoginValidation.LoginWarnings.Add(LoginWarning.UserIsDisallowedToVote);

            if (_serviceCheckUserAlreadyVotedService.IsUserVoted())
                LoginValidation.LoginWarnings.Add(LoginWarning.UserAlreadyVoted);

            return LoginValidation.Warning;
        }
    }
}