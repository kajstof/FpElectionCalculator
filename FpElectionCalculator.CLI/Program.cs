using System;
using System.Collections.Generic;
using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Models;

namespace FpElectionCalculator.CLI
{
    static class Program
    {
        static void Main(string[] args)
        {
            ElectionCalculator.Run();
            Domain.Models.User user = new Domain.Models.User(new LoginCredentials("Adam", "Adamski", "86030218897"));
            LoginValidator loginValidator = user.Login();
            if (!loginValidator.Error)
            {
                IEnumerable<Candidate> candidates = ElectionCalculator.GetCandidates();
                user.DoVote(candidates);
            }
            else
            {
                Console.WriteLine("Login error");
                foreach (var loginError in loginValidator.LoginErrors)
                {
                    Console.WriteLine($"Error: {loginError}");
                }
            }
        }
    }
}
