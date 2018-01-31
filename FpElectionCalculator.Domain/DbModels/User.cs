using System.ComponentModel.DataAnnotations;

namespace FpElectionCalculator.Domain.DbModels
{
    class User
    {
        public int UserId { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(11)]
        public string Pesel { get; set; }
        public Vote Vote { get; set; }
        public string Comment { get; set; }
    }
}
