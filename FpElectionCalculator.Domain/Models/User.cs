using System;
using System.Collections.Generic;
using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Interfaces;
using FpElectionCalculator.Domain.Services;

namespace FpElectionCalculator.Domain.Models
{
    public class User : DbModels.User
    {
        private readonly LoginCredentials _loginCredentials;
        private readonly ElectionDbContext _context;
        private readonly WebserviceRawCommunication _webservice;
        private readonly ICheckUserAlreadyVotedService _checkUserAlreadyVotedService;
        private readonly ICheckUserIsDisallowedToVoteService _checkUserIsDisallowdToVoteService;
        public bool Logged { get; private set; }
        private readonly ILoginValidatorService _loginValidatorService;

        public User(LoginCredentials loginCredentials, ElectionDbContext context, WebserviceRawCommunication webservice)
        {
            _loginCredentials = loginCredentials;
            FirstName = loginCredentials.FirstName;
            LastName = loginCredentials.LastName;
            Pesel = loginCredentials.Pesel;
            _context = context;
            _webservice = webservice;
            _checkUserAlreadyVotedService = new CheckUserAlreadyVotedFromDbService(loginCredentials, context);
            _checkUserIsDisallowdToVoteService = new CheckUserIsDisallowedToVoteService(webservice);
            _loginValidatorService = new LoginValidatorService(loginCredentials.FirstName, loginCredentials.LastName,
                Pesel, _checkUserIsDisallowdToVoteService, _checkUserAlreadyVotedService);
        }

        public LoginValidation Login()
        {
            _loginValidatorService.Validate();
            if (!_loginValidatorService.LoginValidation.Error)
            {
                CreateUserIfNotExists();
                Logged = true;
            }

            return _loginValidatorService.LoginValidation;
        }

        public bool Vote(IEnumerable<Candidate> candidateList) =>
            new VoteToDbService(Logged, _loginCredentials, _loginValidatorService, _context, _webservice).Vote(
                candidateList);

        public bool Logout() => Logged = false;

        private bool CreateUserIfNotExists() => new CreateUserInDbService(_context).CreateUser(_loginCredentials);
    }
}