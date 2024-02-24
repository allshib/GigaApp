using FluentAssertions;
using GigaApp.Domain.Authentication;
using Microsoft.Extensions.Options;
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
        private readonly Mock<IAuthenticationStorage> storage;
        private readonly ISetup<IAuthenticationStorage, Task<RecognizedUser?>> findUserSetup;
        private readonly Mock<IOptions<AuthenticationConfiguration>> options;
        private AuthenticationService sut;

        public AuthenticationServiceShould()
        {
            storage = new Mock<IAuthenticationStorage>();
            findUserSetup = storage.Setup(x => x.FindUser(It.IsAny<string>(), It.IsAny<CancellationToken>()));

            var securityManager = new Mock<ISecurityManager>();
            securityManager.Setup(x => x.ComparePasswords(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            options = new Mock<IOptions<AuthenticationConfiguration>>();

            options.Setup(x => x.Value).Returns(
                new AuthenticationConfiguration
                {
                    Key = "QkEeenXpHqqP6t0WwpUetAFvUUZiMb4f",
                    Iv = "dtEzMsz2ogg="
                });



            sut = new AuthenticationService(storage.Object, securityManager.Object, options.Object);
        }
        


        [Fact]
        public async Task ReturnSuccess_WhenUserFound()
        {
            findUserSetup.ReturnsAsync(new RecognizedUser
            {
                Salt = "5FfG/0BjsgHLk21YRuMlKg==",
                PasswordHash = "78t/prDszng2ZHPjpW9Fe/o0sfncIPbiLihovOf7c20+eo1O95m+KHklazEVr1KnZIHGE3mc3Kn+L6e86KVPDtKnlcxzvKVS/sq3Dh6fl1SAbrhsn3ZBm+2TVY/lfa7j6ha9UQ==",
                UserId = Guid.Parse("e25d0acc-c2ce-4401-a147-7ff7eb0cee0f")
            });

            var (success, authToken) = await sut.SignIn(new BasicSignInCredentials("User", "Password"), CancellationToken.None);

            success.Should().Be(true);
            authToken.Should().NotBeEmpty();
        }


        [Fact]
        public async Task AuthenticateUser_AfterTheySignIn()
        {
            var userId = Guid.Parse("28a72e9f-5883-44a0-8af7-eaa37af98d44");
            findUserSetup.ReturnsAsync(new RecognizedUser
            {
                Salt = "5FfG/0BjsgHLk21YRuMlKg==",
                PasswordHash = "78t/prDszng2ZHPjpW9Fe/o0sfncIPbiLihovOf7c20+eo1O95m+KHklazEVr1KnZIHGE3mc3Kn+L6e86KVPDtKnlcxzvKVS/sq3Dh6fl1SAbrhsn3ZBm+2TVY/lfa7j6ha9UQ==",
                UserId = userId
            });

            var (success, authToken) = await sut.SignIn(new BasicSignInCredentials("User", "Password"), CancellationToken.None);

            var identity = await sut.Authenticate(authToken, CancellationToken.None);

            identity.UserId.Should().Be(userId);
        }


        [Fact]
        public async Task SignInUser_WhenPasswordMatches()
        {
            var password = "querty";

            var secManager = new SecurityManager();
            var (salt, hash) = secManager.GeneratePasswordParts(password);

            findUserSetup.ReturnsAsync(new RecognizedUser {
                UserId = Guid.Parse("052f9629-9df4-4ffe-95e1-9c8f59c5f955"),
                Salt = salt,
                PasswordHash = hash
            });


            var localSut = new AuthenticationService(storage.Object, secManager, options.Object);

            var (success, _ ) = await localSut.SignIn(new BasicSignInCredentials("User", password), CancellationToken.None);

            success.Should().BeTrue();
        }
    }



    
}
