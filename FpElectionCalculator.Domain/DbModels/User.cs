using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

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

        private string _pesel;
        public string Pesel
        {
            //get => StringCipher.Decrypt(_pesel, "s@r89fSDn30S@$(");
            //set => _pesel = StringCipher.Encrypt(value, "s@r89fSDn30S@$(");
            get => _pesel;
            set => _pesel = value;
        }

        public int? CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        //public int PartyId { get; set; }
        //public Party Party { get; set; }
        //public int VoteId { get; set; }
        //public Vote Vote { get; set; }
        public string Comment { get; set; }
    }
}
