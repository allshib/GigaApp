using GigaApp.Domain.Authentication;
using GigaApp.Domain.UseCases.SignOn;
using Moq;
using Moq.Language.Flow;
using FluentAssertions;

namespace GigaApp.Domain.Tests.SignOn
{
    public class SignOnUseCaseShould
    {
        private readonly Mock<IPasswordManager> passwordManager;
        private readonly ISetup<IPasswordManager, (byte[] Salt, byte[] Hash)> generatePasswordPartsSetup;
        private readonly ISetup<ISignOnStorage, Task<Guid>> storageCreateUserSetup;
        private readonly SignOnUseCase sut;
        private readonly Mock<ISignOnStorage> storage;

        public SignOnUseCaseShould()
        {

            storage = new Mock<ISignOnStorage>();
            //var validator = new Mock<IValidator<SignOnCommand>>();
            //validator
            //    .Setup(x => x.ValidateAsync(It.IsAny<SignOnCommand>(), It.IsAny<CancellationToken>()))
            //    .ReturnsAsync(new ValidationResult());

            passwordManager = new Mock<IPasswordManager>();

            generatePasswordPartsSetup = passwordManager
                .Setup(x => x.GeneratePasswordParts(It.IsAny<string>()));

            storageCreateUserSetup = storage
                .Setup(x => x.CreateUser(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>(),
                    It.IsAny<CancellationToken>()));
                //.ReturnsAsync(new User(Guid.Parse("40e49942-3d4c-4151-b2f4-a5468944f885")));

            sut = new SignOnUseCase(storage.Object, passwordManager.Object);
        }

        [Fact]
        public async Task ReturnIdentityOfNewlyCreatedUser()
        {
            var userId = Guid.Parse("40e49942-3d4c-4151-b2f4-a5468944f885");
            var salt = new byte[]{1};
            var hash = new byte[]{2};

            generatePasswordPartsSetup.Returns((salt, hash));
            storageCreateUserSetup.ReturnsAsync(userId);

            var command = new SignOnCommand("Test", "qwerty");

            var actual = await sut.Handle(command, CancellationToken.None);

            actual.UserId.Should().Be(userId);
        }
        

        [Fact]
        public async Task CreateUser_WithGeneratePasswordParts()
        {
            var salt = new byte[] { 1 };
            var hash = new byte[] { 2 };
            generatePasswordPartsSetup.Returns((salt, hash));

            var command = new SignOnCommand("Test", "qwerty");

           await sut.Handle(command, CancellationToken.None);

            passwordManager.Verify(x => x.GeneratePasswordParts("qwerty"), Times.Once);

            storage.Verify(x => x.CreateUser("Test", salt, hash, It.IsAny<CancellationToken>()), Times.Once);
            storage.VerifyNoOtherCalls();
        }
    }
}
