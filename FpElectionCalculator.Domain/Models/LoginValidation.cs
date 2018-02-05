using System.Collections.Generic;
using System.Collections.ObjectModel;
using FpElectionCalculator.Domain.Services;

namespace FpElectionCalculator.Domain.Models
{
    public class LoginValidation
    {
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly Pesel _pesel;
        private readonly IUserIsDisallowedToVoteService _serviceUserIsDisallowedToVoteService;
        private readonly IUserAlreadyVotedService _serviceUserAlreadyVotedService;

        public bool Warning => LoginWarnings.Count > 0;
        public ICollection<LoginWarning> LoginWarnings { get; } = new Collection<LoginWarning>();

        public bool Error => LoginErrors.Count > 0;
        public ICollection<LoginError> LoginErrors { get; } = new Collection<LoginError>();

        public LoginValidation(string firstName, string lastName, Pesel pesel,
            IUserIsDisallowedToVoteService serviceUserIsDisallowedToVoteService, IUserAlreadyVotedService serviceUserAlreadyVotedService)
        {
            _firstName = firstName;
            _lastName = lastName;
            _pesel = pesel;
            _serviceUserIsDisallowedToVoteService = serviceUserIsDisallowedToVoteService;
            _serviceUserAlreadyVotedService = serviceUserAlreadyVotedService;
        }

        private bool Validate()
        {
            if (!_pesel.IsValid())
                LoginErrors.Add(LoginError.PeselIsNotValid);
            else if (!_pesel.IsEighteen())
                LoginWarnings.Add(LoginWarning.UserIsNotEighteen);

            if (_firstName.Length < 2)
                LoginErrors.Add(LoginError.UserFirstNameIsTooShort);

            if (_lastName.Length < 2)
                LoginErrors.Add(LoginError.UserLastNameIsTooShort);

            if (_serviceUserIsDisallowedToVoteService.IsPeselDisallowedToVote(_pesel.ToString()))
                LoginWarnings.Add(LoginWarning.UserIsDisallowedToVote);
//            if (WebserviceLogic.IsPeselDisallowedToVote(_pesel.ToString()))

            if (_serviceUserAlreadyVotedService.IsUserVoted(new LoginCredentials(_firstName, _lastName, _pesel.ToString())))
                LoginWarnings.Add(LoginWarning.UserAlreadyVoted);
            //if (DatabaseLogic.IsUserVoted(new LoginCredentials(_firstName, _lastName, _pesel.ToString())))

            return LoginErrors.Count == 0;
        }
    }
}