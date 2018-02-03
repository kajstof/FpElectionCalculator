using FpElectionCalculator.Domain.DbModels;
using FpElectionCalculator.Domain.Services;
using System;
using System.Collections.Generic;

namespace FpElectionCalculator.Domain
{
    public class ElectionCalculator
    {
        public static void Run()
        {
            WebserviceRawCommunication webservice = new WebserviceRawCommunication();
            WebserviceDataParser parser = new WebserviceDataParser();
            JsonModels.CandidatesList candidatesList = parser.GetCandidatesList(webservice.GetCandidates());

            JsonToDbConverter jsonConverter = new JsonToDbConverter();
            jsonConverter.ConvertPartiesAndCandidatesFromJsonModelToDbModel(candidatesList);
            
            using (var context = new ElectionDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Add Parties
                Party par1 = new Party() { Name = "Niebiescy" };
                Party par2 = new Party() { Name = "Różowi" };
                Party par3 = new Party() { Name = "Fioletowi" };
                context.Add(par1);
                context.Add(par2);
                context.Add(par3);

                // Add Candidates
                Candidate cnd1 = new Candidate() { Name = "Kazimierz X", Party = par1 };
                Candidate cnd2 = new Candidate() { Name = "Kazimierz XI", Party = par1 };
                Candidate cnd3 = new Candidate() { Name = "Kazimierz XII", Party = par1 };
                Candidate cnd4 = new Candidate() { Name = "Zygmunt I", Party = par2 };
                Candidate cnd5 = new Candidate() { Name = "Zygmunt II", Party = par2 };
                Candidate cnd6 = new Candidate() { Name = "Zygmunt III", Party = par2 };
                Candidate cnd7 = new Candidate() { Name = "Zygmunt IV", Party = par2 };
                Candidate cnd8 = new Candidate() { Name = "Zygmunt V", Party = par2 };
                context.Add(cnd1);
                context.Add(cnd2);
                context.Add(cnd3);
                context.Add(cnd4);
                context.Add(cnd5);
                context.Add(cnd6);
                context.Add(cnd7);
                context.Add(cnd8);

                // Add Users
                context.Add(new User() { FirstName = "Krzysztof", LastName = "Krysiak", Pesel = "12345678912"});
                context.Add(new User() { FirstName = "Jan", LastName = "Nowak", Pesel = "11111222223", Comment = "Comment" });
                context.Add(new User() { FirstName = "Darek", Comment = "Comment some", LastName = "Hifsai", Pesel = "12312312" });

                // Save changes
                context.SaveChanges();
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