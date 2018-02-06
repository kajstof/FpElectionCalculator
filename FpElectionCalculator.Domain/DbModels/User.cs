using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FpElectionCalculator.Domain.Utils;

namespace FpElectionCalculator.Domain.DbModels
{
    [Table("Users")]
    public class User
    {
        public int UserId { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public string Comment { get; set; }

        private string _hash = "FuTur3pr0C3$s!nG#";
        public string PeselHashed { get; set; }
        [NotMapped]
        public string Pesel
        {
            get => StringCipher.Decrypt(PeselHashed, _hash);
            set => PeselHashed = StringCipher.Encrypt(value, _hash);
        }

        public bool Voted { get; set; }
        public ICollection<Vote> Votes { get; set; }

        //public int? CandidateID { get; set; }
        //public Candidate Candidate { get; set; }
        //public int PartyID { get; set; }
        //public Party Party { get; set; }
    }
}
