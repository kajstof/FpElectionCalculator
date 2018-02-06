﻿namespace FpElectionCalculator.Domain.Models
{
    public enum LoginError
    {
        PeselIsNotValid,
        UserFirstNameIsTooShort,
        UserFirstNameContainIllegalChars,
        UserLastNameIsTooShort,
        UserLastNameContainIllegalChars
    }
}
