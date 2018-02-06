using System;
using System.Collections.Generic;
using System.Linq;
using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Services;
using FpElectionCalculator.Domain.Utils;

namespace FpElectionCalculator.Domain.Models
{
    public class LoginCredentials
    {
        public LoginCredentials(string firstName, string lastName, string pesel)
        {
            FirstName = firstName.CapitalizeAllWords();
            if (!firstName.Equals(FirstName))
            {
                ConsoleColor consoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Your first name is changed to: \"{FirstName}\"");
                Console.ForegroundColor = consoleColor;
            }

            LastName = lastName.CapitalizeAllWords();
            if (!lastName.Equals(LastName))
            {
                ConsoleColor consoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Your last name is changed to: \"{LastName}\"");
                Console.ForegroundColor = consoleColor;
            }
            
            Pesel = pesel;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Pesel { get; private set; }
    }
}