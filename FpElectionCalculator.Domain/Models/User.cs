using System;
using System.Collections.Generic;

namespace FpElectionCalculator.Domain.Models
{
    public class User : DbModels.User
    {
        private new Pesel Pesel { get; }

        public User(string firstName, string lastName, string pesel)
        {
            FirstName = firstName;
            LastName = lastName;
            Pesel = new Pesel(pesel);
        }

        public LoginValidator Login()
        {
            LoginValidator loginValidator = new LoginValidator(FirstName, LastName, Pesel);
            return loginValidator;
        }

        public bool Logout() => throw new NotImplementedException();
        public bool DoVote(IEnumerable<Candidate> candidateList) => throw new NotImplementedException();
        public bool IsOnBlacklist() => throw new NotImplementedException();
        public bool IsEighteen() => throw new NotImplementedException();
    }
}
