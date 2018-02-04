using System;
using System.Collections.Generic;
using System.Linq;
using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Services;

namespace FpElectionCalculator.Domain.Models
{
    public class User : DbModels.User
    {
        private LoginCredentials _loginCredentials;
        private new Pesel Pesel { get; }

        public User(LoginCredentials loginCredentials)
        {
            _loginCredentials = loginCredentials;
            FirstName = loginCredentials.FirstName;
            LastName = loginCredentials.LastName;
            base.Pesel = loginCredentials.Pesel;
            Pesel = new Pesel(loginCredentials.Pesel);
        }

        public LoginValidator Login()
        {
            LoginValidator loginValidator = new LoginValidator(FirstName, LastName, Pesel);
            if (!loginValidator.Error)
                CreateUserIfNotExists();
            return loginValidator;
        }

        private void CreateUserIfNotExists() => DatabaseLogic.CreateUserIfNotExists(_loginCredentials);
        public bool Logout() => throw new NotImplementedException();

        public bool DoVote(IEnumerable<Candidate> candidateList) =>
            DatabaseAndWebserviceLogic.DoVote(_loginCredentials, candidateList);

        public bool IsPeselDisallowedToVote() => WebserviceLogic.IsPeselDisallowedToVote(Pesel.ToString());
        public bool IsUserVoted() => DatabaseLogic.IsUserVoted(_loginCredentials);
    }
}