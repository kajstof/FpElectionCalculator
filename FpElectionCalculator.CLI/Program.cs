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
            ElectionCalculator.Run();
        }
    }
}
