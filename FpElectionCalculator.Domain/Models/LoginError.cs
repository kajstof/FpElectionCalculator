namespace FpElectionCalculator.Domain.Models
{
    public enum LoginError
    {
        PeselIsNotValid,
        UserIsNotEighteen,
        UserFirstNameIsTooShort,
        UserLastNameIsTooShort,
        UserIsDisallowedToVote,
        UserAlreadyVoted
    }
}
