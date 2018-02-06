using System;
using System.IO;
using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Models;
using FpElectionCalculator.Domain.Services;
using Microsoft.Extensions.Configuration;

namespace FpElectionCalculator.CLI
{
    class Program
    {
        private static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            // Preparing services
            var context = new ElectionDbContext(Configuration.GetConnectionString("FpElectionDatabase"));
            using (context)
            {
                var webservice = new WebserviceRawCommunication();

                // Try to create database (if not exists) and populate with candidates data
                var dbInitializer = new DatabaseInitializer(context, new GetJsonCandidateListFromWsService(webservice));
                //dbInitializer.DeleteTablesInDatabase();
                dbInitializer.InitializeDbWithCandidatesAndParties();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("==================================================");
                Console.WriteLine("== Election Calculator - Future Processing =======");
                Console.WriteLine("== Author: Krzysztof Krysiak ============= 2018 ==");
                Console.WriteLine("==================================================");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("First step - you must log in");
                LoginValidation loginValidation;
                do
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Please enter your first name: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    string firstName = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Please enter your last name: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    string lastName = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Please enter your PESEL: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    string pesel = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    LoginCredentials loginCredentials = new LoginCredentials(firstName, lastName, pesel);
                    Domain.Models.User user = new Domain.Models.User(loginCredentials, context, webservice);
                    loginValidation = user.Login();

                    if (loginValidation.Error)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        foreach (var error in loginValidation.LoginErrors)
                        {
                            Console.WriteLine($"Error: {LoginErrorDescription.GetDescription(error)}");
                        }
                    }

                    if (loginValidation.Warning)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        foreach (var warning in loginValidation.LoginWarnings)
                        {
                            Console.WriteLine($"Error: {LoginWarningDescription.GetDescription(warning)}");
                        }
                    }

                    if (!loginValidation.Error)
                    {

                    }

                    //                {
                    //                    IEnumerable<Candidate> candidates = electionCalculator.GetCandidates();
                    //                    user.DoVote(candidates);
                    //                }
                    //                else
                    //                {
                    //                    Console.WriteLine("Login error");
                    //                    foreach (var loginError in loginValidation.LoginErrors)
                    //                    {
                    //                        Console.WriteLine($"Error: {loginError}");
                    //                    }
                    //                }
                } while (loginValidation.Error);
            }
        }
    }
}