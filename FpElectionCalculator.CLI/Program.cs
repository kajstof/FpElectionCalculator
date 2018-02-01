using FpElectionCalculator.Domain;
using FpElectionCalculator.Domain.Models;
using System;

namespace FpElectionCalculator.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            string firstName = "Krzysztof";
            string lastName = "Krysiak";
            string pesel = "86030218897";
            ElectionCalculator el = new ElectionCalculator();
            el.Run();
            User user = new User(firstName, lastName, pesel);
            if (user.Login())
            {
                // User logged
            }
            else
            {
                // User not logged
            }
        }
    }
}
