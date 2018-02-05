using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FpElectionCalculator.Domain.JsonModels
{
    public class CandidatesList
    {
        [JsonProperty("publicationDate")]
        public DateTime PublicationDate { get; set; }
        [JsonProperty("candidate")]
        public IList<Candidate> CandidateList { get; set; }

        public IList<DbModels.Party> ConvertToDbModel()
        {
            IEnumerable<string> partiesJson = (from candidate in CandidateList
                select candidate.Party).Distinct();
            IList<DbModels.Party> partiesDb = new List<DbModels.Party>();
            foreach (var party in partiesJson)
            {
                var candidatesInParty = (from candidate in CandidateList
                    where candidate.Party == party
                    select new DbModels.Candidate() {Name = candidate.Name}).ToList();
                partiesDb.Add(new DbModels.Party() {Name = party, Candidates = candidatesInParty});
            }

            return partiesDb;
        }
    }
}
