using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

                KeyOption key;
                do
                {
                    key = ReadKeyOption();
                    switch (key)
                    {
                        case KeyOption.Login:
                            LoginUserMethod(context, webservice);
                            break;
                        case KeyOption.Quit:
                            break;
                    }
                } while (key != KeyOption.Quit);
            }
        }

        private static void LoginUserMethod(ElectionDbContext context, WebserviceRawCommunication webservice)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("----- Login to system -----");
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

                    if (!loginValidation.LoginWarnings.Contains(LoginWarning.UserAlreadyVoted))
                    {
                        IList<Candidate> candidates = context.Candidates.ToList();
                        IList<Party> parties = context.Parties.ToList();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        for (int i = 0; i < candidates.Count; i++)
                        {
                            Console.WriteLine(
                                $"{i + 1}] {candidates[i].Name} / {parties.Single(x => x.PartyId == candidates[i].PartyId).Name}");
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
                            Console.WriteLine(
                                $"You voted to {c.Name} / {parties.Single(x => x.PartyId == c.PartyId).Name}");

                        user.Vote(candidates);

                        // Get Results TODO
                        GetDbVotesList votesService = new GetDbVotesList(context);
                        votesService.GetVoteStatistics();
                    }

                    user.Logout();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("- User logout");
                }
            }
        }

        private static KeyOption ReadKeyOption()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Program options:");
            Console.WriteLine("1] Login");
            Console.WriteLine("q] Quit");
            Console.ForegroundColor = ConsoleColor.White;
            char[] options = {'Q', 'q', '1'};
            char key;
            do
            {
                Console.Write("What are you going to do: ");
                key = Console.ReadKey().KeyChar;
                Console.WriteLine();
            } while (!options.Any(c => c.Equals(key)));

            switch (key)
            {
                case '1':
                    return KeyOption.Login;
                default:
                    return KeyOption.Quit;
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

    internal enum KeyOption
    {
        Quit,
        Login
    }
}