using System;
using System.Collections.Generic;
using System.Text;

namespace FpElectionCalculator.Domain.Models
{
    public class User
    {
        public User(string firstName, string lastName, string pesel)
        {
            FirstName = firstName;
            LastName = lastName;
            Pesel = pesel;
        }

        public bool Login()
        {
            throw new NotImplementedException();
        }
        
        public bool Logout()
        {
            throw new NotImplementedException();
        }

        public bool DoVote(Candidate candidate, Party party)
        {
            throw new NotImplementedException();
        }

        public bool IsOnBlacklist()
        {
            throw new NotImplementedException();
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string Pesel { get; }
        public string Voted { get; }
        public Vote Vote { get; }
        public string Comment { get; }
    }
}
