using System;
using System.Collections.Generic;
using System.Text;

namespace FpElectionCalculator.Domain.Models
{
    public class User
    {
        public string FirstName { get; }
        public string LastName { get; }
        public Pesel Pesel { get; }
        public bool HaveVoted { get; }
        public Vote Vote { get; }
        public string Comment { get; }

        public User(string firstName, string lastName, string peselString)
        {
            FirstName = firstName;
            LastName = lastName;
            Pesel = new Pesel(peselString);
        }

        public bool Login() => throw new NotImplementedException();
        public bool Logout() => throw new NotImplementedException();
        public bool DoVote(Candidate candidate, Party party) => throw new NotImplementedException();
        public bool IsOnBlacklist() => throw new NotImplementedException();
        public bool IsEighteen() => throw new NotImplementedException();
    }
}
