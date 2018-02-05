using System;
using System.Collections.Generic;
using System.IO;
using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace FpElectionCalculator.CLI
{
    static class Program
    {
//        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
//            var builder = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddXmlFile("App.config");
//
//            Configuration = builder.Build();
//
//            ElectionDbContext context = new ElectionDbContext(Configuration["FpElectionDatabase"]);
            ElectionDbContext context = new ElectionDbContext("dd");

            ElectionCalculator electionCalculator = new ElectionCalculator(context);
            electionCalculator.Run();

//            Domain.Models.User user = new Domain.Models.User(new LoginCredentials("Adam", "Adamski", "86030218897"), context);
//            LoginValidation loginValidation = user.Login();
//            if (!loginValidation.Error)
//            {
//                IEnumerable<Candidate> candidates = electionCalculator.GetCandidates();
//                user.DoVote(candidates);
//            }
//            else
//            {
//                Console.WriteLine("Login error");
//                foreach (var loginError in loginValidation.LoginErrors)
//                {
//                    Console.WriteLine($"Error: {loginError}");
//                }
//            }
        }
    }
}
