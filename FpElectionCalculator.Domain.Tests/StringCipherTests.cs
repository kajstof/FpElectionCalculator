using FpElectionCalculator.Domain.Services;
using Xunit;

namespace FpElectionCalculator.Domain.Tests
{
    public class StringCipherTests
    {
        private string _hash = "FuTur3pr0C3$s!nG#";

        [Theory]
        [InlineData("12345678901")]
        [InlineData("Some example text to encrypt")]
        public void ReturnsEncryptedString(string text)
        {
            string encryptedText = StringCipher.Encrypt(text, _hash);
            Assert.NotEqual(encryptedText, text);
        }

        [Theory]
        [InlineData("fdsajkfFUISFhAS*fsd8f9FY*(HWEUrfweF&SEbjsdfSSE&*F")]
        [InlineData("gdnS*SD(FENekwr8(SDFYS*(Fyweew7F^&GSEF^&^SEfgsef&")]
        public void ReturnsDecryptetString(string text)
        {
            string decryptedText = StringCipher.Encrypt(text, _hash);
            Assert.NotEqual(decryptedText, text);
        }

        [Theory]
        [InlineData("12345678901")]
        [InlineData("Some example text to encrypt")]
        public void EncryptedAndDecryptedTextAreEqual(string text)
        {
            string encryptedText = StringCipher.Encrypt(text, _hash);
            string decryptedText = StringCipher.Decrypt(encryptedText, _hash);
            Assert.Equal(text, decryptedText);
        }
    }
}
