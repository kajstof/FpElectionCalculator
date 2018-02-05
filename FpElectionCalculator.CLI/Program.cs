using System;
using System.Collections.Generic;
using System.IO;
using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Models;
using FpElectionCalculator.Domain.Services;
using Microsoft.Extensions.Configuration;

namespace FpElectionCalculator.CLI
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder() .SetBasePath(Directory.GetCurrentDirectory()) .AddXmlFile("App.config");
            Configuration = builder.Build();

            // Preparing services
            ElectionDbContext context = new ElectionDbContext(Configuration["FpElectionDatabase"]);
            WebserviceRawCommunication webservice = new WebserviceRawCommunication();

            // Try to create database (if not exists) and populate with candidates data
            DatabaseInitializer dbInitializer = new DatabaseInitializer(context, new GetJsonCandidateListService(webservice));

            //
            //ElectionDbContext context = new ElectionDbContext("dd");
            //ElectionCalculator electionCalculator = new ElectionCalculator(context);
            //electionCalculator.Run();

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
