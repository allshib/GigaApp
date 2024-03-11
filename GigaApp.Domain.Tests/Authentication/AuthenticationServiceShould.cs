using FluentAssertions;
using GigaApp.Domain.Authentication;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Tests.Authentication
{
    public class AuthenticationServiceShould
    {
        private readonly ISetup<IAuthenticationStorage, Task<Session?>> findSessionSetup;
        private readonly AuthenticationService sut;
        private readonly ISetup<ISymmetricDecryptor, Task<string>> decryptorSetup;

        public AuthenticationServiceShould()
        {
            var decryptor = new Mock<ISymmetricDecryptor>();
            decryptorSetup = decryptor.Setup(x => x.Decrypt(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<CancellationToken>()));

            var options = new Mock<IOptions<AuthenticationConfiguration>>();
            options.Setup(x => x.Value).Returns(new AuthenticationConfiguration
            {
                Base64Key = "NhB8TBbwVkihKXK2BgxyWPne8mw1nTey88xKjT72N6U="
            });
            //var passwordManager = new Mock<IPasswordManager>();

            var storage = new Mock<IAuthenticationStorage>();
            findSessionSetup = storage.Setup(x => x.FindSession(It.IsAny<Guid>(), It.IsAny<CancellationToken>())); 

            sut = new AuthenticationService(decryptor.Object, options.Object, storage.Object, NullLogger<AuthenticationService>.Instance);
        }
        [Fact]
        public async Task ReturnGuestIdentity_WhenSessionNotFound()
        {
            decryptorSetup.ReturnsAsync("fd9f63eb-8048-4028-88aa-499c89177595");
            findSessionSetup.ReturnsAsync(() => null);

            var actual = await sut.Authenticate("good-token", CancellationToken.None);

            actual.Should().BeEquivalentTo(User.Guest);
        }

        [Fact]
        public async Task ReturnGuestIdentity_WhenSessionIsExpired()
        {
            decryptorSetup.ReturnsAsync("fd9f63eb-8048-4028-88aa-499c89177595");
            findSessionSetup.ReturnsAsync(() => new Session
            {
                ExpiresAt = DateTimeOffset.UtcNow.AddDays(-10)
            });

            var actual = await sut.Authenticate("good-token", CancellationToken.None);

            actual.Should().BeEquivalentTo(User.Guest);
        }
        [Fact]
        public async Task ReturnGuestIdentity_WhenTokenCannotBeDecrypted()
        {
            decryptorSetup.Throws<CryptographicException>();

            var actual = await sut.Authenticate("bad token", CancellationToken.None);

            actual.Should().BeEquivalentTo(User.Guest);
        }
        [Fact]
        public async Task ReturnIdentity_WhenTokenIsValid()
        {
            var sessionId = Guid.Parse("550f33cf-345d-4814-8e39-0469c9ee9c8d");
            var userId = Guid.Parse("71bd1769-0fcd-4624-8f03-a0af645b2639");

            decryptorSetup.ReturnsAsync("fd9f63eb-8048-4028-88aa-499c89177595");
            findSessionSetup.ReturnsAsync(() => new Session
            {
                Id = sessionId,
                UserId = userId,
                ExpiresAt = DateTimeOffset.UtcNow.AddDays(1)
            });

            var actual = await sut.Authenticate("good-token", CancellationToken.None);

            actual.Should().BeEquivalentTo(new User(userId, sessionId));

        }



    }
}
