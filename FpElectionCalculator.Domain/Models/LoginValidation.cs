using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FpElectionCalculator.Domain.Models
{
    public class LoginValidation
    {
        public bool Warning => LoginWarnings.Count > 0;
        public ICollection<LoginWarning> LoginWarnings { get; set; } = new Collection<LoginWarning>();

        public bool Error => LoginErrors.Count > 0;
        public ICollection<LoginError> LoginErrors { get; set; } = new Collection<LoginError>();
    }
}