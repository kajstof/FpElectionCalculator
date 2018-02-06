using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Models;
using FpElectionCalculator.Domain.Services;
using FpElectionCalculator.Domain.Utils;
using Microsoft.Extensions.Configuration;
using User = FpElectionCalculator.Domain.Models.User;

namespace FpElectionCalculator.CLI
{
    class Program
    {
        private static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            ConsoleProlog();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("- Reading application settings - please wait...");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            Console.WriteLine("- Connecting to database - please wait...");
            // Preparing services
            var context = new ElectionDbContext(Configuration.GetConnectionString("FpElectionDatabase"));
            using (context)
            {
                Console.WriteLine("- Initializing webservice - please wait...");
                var webservice = new WebserviceRawCommunication();
                Console.WriteLine("- Initializing database - please wait...");
                // Try to create database (if not exists) and populate with candidates data
                var dbInitializer = new DatabaseInitializer(context, new GetJsonCandidateListFromWsService(webservice));
                //dbInitializer.DeleteTablesInDatabase();
                dbInitializer.InitializeDbWithCandidatesAndParties();

                Console.ForegroundColor = ConsoleColor.Yellow;
                //Console.WriteLine("Program options:");
                //Console.WriteLine("1] Login");
                //Console.WriteLine("q] Quit");
                //Console.Write("What are you going to do: ");
                do
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("- Please login");
                    LoginCredentials loginCredentials = GetCredentialsFromConsole();
                    User user = new Domain.Models.User(loginCredentials, context, webservice);
                    LoginValidation loginValidation = user.Login();

                    if (loginValidation.Error)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        foreach (var error in loginValidation.LoginErrors)
                        {
                            Console.WriteLine($"- Error: {LoginErrorDescription.GetDescription(error)}");
                        }
                    }

                    if (loginValidation.Warning)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        foreach (var warning in loginValidation.LoginWarnings)
                        {
                            Console.WriteLine($"- Warning: {LoginWarningDescription.GetDescription(warning)}");
                        }
                    }

                    if (!loginValidation.Error)
                    {
                        if (user.Logged)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"- User logged : {user.FirstName} / {user.LastName} / {user.Pesel}");

                            if (user.Voted)
                            {
                                Console.WriteLine("You've already voted to:");
                                if (user.Votes.Count != 0)
                                {
                                    foreach (var vote in user.Votes)
                                    {
                                        Console.WriteLine($"{vote.Candidate.Name} / {vote.Candidate.Party.Name}");
                                    }
                                }
                                else
                                    Console.WriteLine("Empty vote");
                            }
                            else
                            {
                                IList<Candidate> candidates = context.Candidates.ToList();
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                for (int i = 0; i < candidates.Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}] {candidates[i].Name}");
                                }

                                int[] parsedChoice;
                                do
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.Write("Please enter you candidate (or candidates) no [eg. \"1,5,10\"]: ");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    string candidatesRead = Console.ReadLine();
                                    parsedChoice = ChoiceParser.Parse(candidatesRead, candidates.Count);
                                    if (parsedChoice == null)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine($"- Error: Illegal characters in your response");
                                    }
                                } while (parsedChoice == null);

                                Console.ForegroundColor = ConsoleColor.Yellow;

                                for (int i = candidates.Count; i > 0; i--)
                                {
                                    if (!parsedChoice.Any(x => x == i))
                                        candidates.RemoveAt(i - 1);
                                }

                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                if (candidates.Count == 0)
                                    Console.WriteLine($"You voted to nobody");

                                foreach (var c in candidates)
                                    Console.WriteLine($"You voted to {c.Name} / {c.Party.Name}");

                                user.Vote(candidates);

                                // Get Results
                            }

                            user.Logout();
                        }
                    }
                } while (true);
            }
        }

        private static void ConsoleProlog()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==================================================");
            Console.WriteLine("== Election Calculator - Future Processing =======");
            Console.WriteLine("== Author: Krzysztof Krysiak | 2018 ==============");
            Console.WriteLine("==================================================");
        }

        private static LoginCredentials GetCredentialsFromConsole()
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

            LoginCredentials loginCredentials = new LoginCredentials(firstName, lastName, pesel);
            return loginCredentials;
        }
    }
}