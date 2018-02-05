﻿using System;
using System.Collections.Generic;
using System.Linq;
using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Services;

namespace FpElectionCalculator.Domain.Models
{
    public class LoginCredentials
    {
        public LoginCredentials(string firstName, string lastName, string pesel)
        {
            FirstName = firstName;
            LastName = lastName;
            Pesel = pesel;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Pesel { get; private set; }
    }
}