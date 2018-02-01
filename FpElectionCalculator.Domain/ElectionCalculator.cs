using FpElectionCalculator.Domain.DbModels;
using System;
using System.Collections.Generic;

namespace FpElectionCalculator.Domain
{
    public class ElectionCalculator
    {
        public void Run()
        {
            using (var context = new ElectionDbContext())
            {
                context.Add<User>(new User() { FirstName = "Krzysztof", LastName = "Krysiak", Pesel = "12345678912" });
                context.Add<User>(new User() { FirstName = "Jan", LastName = "Nowak", Pesel = "11111222223" });
                context.Add<Party>(new Party() { Name = "Niebiescy" });
                Party par = new Party() { Name = "Różowi" };
                context.Add<Party>(par);
                context.Add<Party>(new Party() { Name = "Fioletowi" });
                context.Add<Candidate>(new Candidate() { Name = "Kazimierz XI", Party = par, });
            }
            //GetCandidatesListFromDb();
        }

        private void GetCandidatesListFromWebservice() => throw new NotImplementedException();
        private void PutCandidatesListToDb() => throw new NotImplementedException();
        private void GetCandidatesListFromDb() => throw new NotImplementedException();
        private IList<Models.Candidate> GetCandidatesFromWebservice() => throw new NotImplementedException();
        private void GeneratePdfFile() => throw new NotImplementedException();
        private void GenerateCsvFile() => throw new NotImplementedException();
        private void GenerateActualReport() => throw new NotImplementedException();
    }
}
