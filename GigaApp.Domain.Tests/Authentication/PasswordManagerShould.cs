using FluentAssertions;
using GigaApp.Domain.Authentication;
using System.Security.Cryptography;
using Xunit.Abstractions;

namespace GigaApp.Domain.Tests.Authentication
{
    public class PasswordManagerShould
    {
        private readonly PasswordManager sut = new();
        private readonly ITestOutputHelper outputHelper;
        private static byte[] emptySalt = Enumerable.Repeat((byte)0, 100).ToArray();
        private static byte[] emptyHash = Enumerable.Repeat((byte)0, 32).ToArray();


        public PasswordManagerShould(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }

        [Theory]
        [InlineData("password")]
        [InlineData("querty123")]
        public void GenerateSaltAndHash( string password)
        {
            var(salt, hash) = sut.GeneratePasswordParts(password);

            salt.Should().HaveCount(100).And.NotBeEquivalentTo(emptySalt);
            //sha256 => 256 бит => 32 байта
            hash.Should().HaveCount(32).And.NotBeEquivalentTo(emptyHash);
        }


        [Fact]
        public void ReturnTrue_WhenPasswordMatch()
        {
            var password = "password";

            var (salt, hash) = sut.GeneratePasswordParts(password);

            sut.ComparePasswords(password, salt, hash).Should().BeTrue();
        }

        [Fact]
        public void ReturnFalse_WhenPasswordDoesntMatch()
        {
            var password = "password";

            var (salt, hash) = sut.GeneratePasswordParts(password);

            sut.ComparePasswords("querty123", salt, hash).Should().BeFalse();
        }


        [Fact]
        public void GiveMeBase64Key()
        {
            outputHelper.WriteLine(Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)));
        }
    }
}
