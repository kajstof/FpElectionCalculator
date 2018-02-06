namespace FpElectionCalculator.Domain.Models
{
    public static class LoginWarningDescription
    {
        public static string GetDescription(LoginWarning loginError)
        {
            string description;

            switch (loginError)
            {
                case LoginWarning.UserIsDisallowedToVote:
                    description = "You don't have rights to vote. You're on the blacklist";
                    break;
                case LoginWarning.UserIsNotEighteen:
                    description = "You're not eighteen, you can't vote";
                    break;
                case LoginWarning.UserAlreadyVoted:
                    description = "You can't vote again";
                    break;
                default:
                    description = "Unknown warning";
                    break;
            }

            return description;
        }

    }
}
