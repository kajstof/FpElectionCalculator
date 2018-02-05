using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FpElectionCalculator.Domain.JsonModels
{
    public class CandidateList
    {
        [JsonProperty("publicationDate")]
        public DateTime PublicationDate { get; set; }
        [JsonProperty("candidate")]
        public ICollection<Candidate> CandidateListProperty { get; set; }

        public ICollection<DbModels.Party> ConvertToDbModel()
        {
            IEnumerable<string> partiesJson = (from candidate in CandidateListProperty
                                               select candidate.Party).Distinct();
            ICollection<DbModels.Party> partyCollection = new List<DbModels.Party>();
            foreach (var party in partiesJson)
            {
                var candidatesInParty = (from candidate in CandidateListProperty
                                         where candidate.Party == party
                                         select new DbModels.Candidate() { Name = candidate.Name }).ToList();
                partyCollection.Add(new DbModels.Party() { Name = party, Candidates = candidatesInParty });
            }

            return partyCollection;
        }
    }
}
