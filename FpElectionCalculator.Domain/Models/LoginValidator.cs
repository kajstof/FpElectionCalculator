using FpElectionCalculator.Domain.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FpElectionCalculator.Domain.Models
{
    public class LoginValidator
    {
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly Pesel _pesel;

        public bool Error { get; }

        public ICollection<LoginError> LoginErrors { get; } = new Collection<LoginError>();

        public LoginValidator(string firstName, string lastName, Pesel pesel)
        {
            _firstName = firstName;
            _lastName = lastName;
            _pesel = pesel;
            Error = Validate();
        }
        
        private bool Validate()
        {
            if (!_pesel.IsValid())
                LoginErrors.Add(LoginError.PeselIsNotValid);

            else if (!_pesel.IsEighteen())
                LoginErrors.Add(LoginError.UserIsNotEighteen);

            if (_firstName.Length < 2)
                LoginErrors.Add(LoginError.UserFirstNameIsTooShort);

            if (_lastName.Length < 2)
                LoginErrors.Add(LoginError.UserLastNameIsTooShort);

            if (WebserviceConnector.IsPeselDisallowedToVote(_pesel.ToString()))
                LoginErrors.Add(LoginError.UserIsDisallowedToVote);

            if (DatabaseConnector.IsUserVoted(_firstName, _lastName, _pesel.ToString()))
                LoginErrors.Add(LoginError.UserAlreadyVoted);

            return LoginErrors.Count == 0;
        }
    }
}
