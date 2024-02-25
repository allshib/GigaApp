using FluentAssertions;
using GigaApp.Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Tests.Authentication
{
    public class AesSymmetricEncryptorDecryptorShould
    {
        private readonly AesSymmetricEncryptorDecryptor sut = new();


        [Fact]
        public async Task ReturnMeaningfulEncryptedString()
        {
            var key = RandomNumberGenerator.GetBytes(32);
            var actual = await sut.Encrypt("Hellow", key, CancellationToken.None);
            actual.Should().NotBeEmpty();
        }

        [Fact]
        public async Task DecryptEncryptedString_WhenKeyIsSame()
        {
            var key = RandomNumberGenerator.GetBytes(32);
            var encrypted = await sut.Encrypt("Hello", key, CancellationToken.None);
            var decrypted = await sut.Decrypt(encrypted, key, CancellationToken.None);
            decrypted.Should().Be("Hello");
        }

        [Fact]
        public async Task ThrowException_WhenDecriptingWithDifferentKey()
        {
            var encrypted = await sut.Encrypt("Hello", RandomNumberGenerator.GetBytes(32), CancellationToken.None);
            await sut.Invoking(
                x=>x.Decrypt(encrypted, RandomNumberGenerator.GetBytes(32), CancellationToken.None))
                .Should().ThrowAsync<CryptographicException>();

        }
    }
}
