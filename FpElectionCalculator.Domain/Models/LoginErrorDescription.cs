namespace FpElectionCalculator.Domain.Models
{
    public static class LoginErrorDescription
    {
        public static string GetDescription(LoginError loginError)
        {
            string description;

            switch (loginError)
            {
                case LoginError.PeselIsNotValid:
                    description = "Pesel is not valid";
                    break;
                case LoginError.UserFirstNameIsTooShort:
                    description = "Your first name is too short";
                    break;
                case LoginError.UserLastNameIsTooShort:
                    description = "Your last name is too short";
                    break;
                default:
                    description = "Unknown error";
                    break;
            }

            return description;
        }
    }
}