using FluentAssertions;
using GigaApp.Domain.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Tests.Authentication
{
    public class AuthenticationServiceShould
    {
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
            var passwordManager = new Mock<IPasswordManager>();

            sut = new AuthenticationService(decryptor.Object, passwordManager.Object, options.Object);
        }

        [Fact]
        public async Task ExtractUserIdentityFromToken()
        {
            decryptorSetup.ReturnsAsync("fd9f63eb-8048-4028-88aa-499c89177595");

            var actual = await sut.Authenticate("some token", CancellationToken.None);

            actual.Should().BeEquivalentTo(new User(Guid.Parse("fd9f63eb-8048-4028-88aa-499c89177595")));
        }


    }
}
