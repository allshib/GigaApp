using System;
using System.Diagnostics.Eventing.Reader;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using GigaApp.Domain.Authentication;
using GigaApp.Domain.Exceptions;
using GigaApp.Domain.UseCases.SignIn;
using GigaApp.Domain.UseCases.SignOn;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Moq;
using Moq.Language.Flow;

namespace GigaApp.Domain.Tests.SignIn;

public class SignInUseCaseShould
{
    private readonly Mock<IPasswordManager> passwordManager;
    private readonly ISetup<IPasswordManager, bool> comparePasswordSetup;
    private readonly ISetup<ISignInStorage, Task<RecognizedUser>> storageFindUserSetup;

    public ISetup<ISignInStorage, Task<Guid>> createSessionSetup { get; }
    internal Mock<ISymmetricEncryptor> encryptor { get; }

    private readonly SignInUseCase sut;
    private readonly Mock<ISignInStorage> storage;
    private readonly ISetup<ISymmetricEncryptor, Task<string>> encryptSetup;

    public SignInUseCaseShould()
    {

        storage = new Mock<ISignInStorage>();
        var validator = new Mock<IValidator<SignInCommand>>();
        validator
            .Setup(x => x.ValidateAsync(It.IsAny<SignInCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        passwordManager = new Mock<IPasswordManager>();
        comparePasswordSetup = passwordManager.Setup(x =>
            x.ComparePasswords(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>()));


        storageFindUserSetup = storage
            .Setup(x => x.FindUser(It.IsAny<string>(), It.IsAny<CancellationToken>()));

        createSessionSetup = storage.Setup(x => x.CreateSession(It.IsAny<Guid>(), It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>()));

        encryptor = new Mock<ISymmetricEncryptor>();
        encryptSetup = encryptor.Setup(x => x.Encrypt(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<CancellationToken>()));
        var options = new Mock<IOptions<AuthenticationConfiguration>>();
        options.Setup(x => x.Value).Returns(new AuthenticationConfiguration
        {
            Base64Key = "NhB8TBbwVkihKXK2BgxyWPne8mw1nTey88xKjT72N6U="
        });

        sut = new SignInUseCase(validator.Object, storage.Object, passwordManager.Object, encryptor.Object,
            options.Object);
    }

    [Fact]
    public async Task ThrowValidationException_WhenUserNotFound()
    {
        storageFindUserSetup.ReturnsAsync(() => null);

        (await sut.Invoking(x => x.Execute(new SignInCommand("Test", "qwerty"), CancellationToken.None))
                .Should().ThrowAsync<ValidationException>())
            .Which.Errors.Should().Contain(e => e.PropertyName == "Login");

    }


    [Fact]
    public async Task ThrowValidationException_WhenPasswordNotMatch()
    {
        storageFindUserSetup.ReturnsAsync(new RecognizedUser());
        comparePasswordSetup.Returns(false);
        var command = new SignInCommand("Test", "qwerty");

        (await sut.Invoking(x => x.Execute(new SignInCommand("Test", "qwerty"), CancellationToken.None))
                .Should().ThrowAsync<ValidationException>())
            .Which.Errors.Should().Contain(e => e.PropertyName == "Password");
    }
    [Fact]
    public async Task CreateSession_WhenPasswordMatches()
    {
        var userId = Guid.Parse("9c78da2b-48e4-480e-8b51-ec63a947cd64");
        storageFindUserSetup.ReturnsAsync(new RecognizedUser { UserId = userId });
        comparePasswordSetup.Returns(true);

        var sessionId = Guid.Parse("19860b88-35fd-4ea7-9527-a3b94645911c");
        createSessionSetup.ReturnsAsync(sessionId);

        var (identity, token) = await sut.Execute(new SignInCommand("Test", "qwerty"), CancellationToken.None);



        token.Should().NotBeEmpty();
        

        storage.Verify(x => x.CreateSession(userId, It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>()), Times.Once);

    }


    [Fact]
    public async Task ReturnTokenAndIdentity()
    {
        var userId = Guid.Parse("54d7dd1f-bd91-425b-a3f8-e2eeba4eb3dc");
        var sessionId = Guid.Parse("19860b88-35fd-4ea7-9527-a3b94645911c");
        createSessionSetup.ReturnsAsync(sessionId);

        storageFindUserSetup.ReturnsAsync(new RecognizedUser
        {
            UserId = userId
        });
        comparePasswordSetup.Returns(true);
        encryptSetup.ReturnsAsync("token");

        var (identity, token) = await sut.Execute(new SignInCommand("Test", "qwerty"), CancellationToken.None);
;       identity.UserId.Should().Be(userId);
        identity.SessionId.Should().Be(sessionId);
        token.Should().Be("token");
    }

    [Fact]
    public async Task EncryptSessionIdIntoToken()
    {
        var userId = Guid.Parse("54d7dd1f-bd91-425b-a3f8-e2eeba4eb3dc");
        var sessionId = Guid.Parse("19860b88-35fd-4ea7-9527-a3b94645911c");
        storageFindUserSetup.ReturnsAsync(new RecognizedUser { UserId = userId});
        createSessionSetup.ReturnsAsync(sessionId);
        comparePasswordSetup.Returns(true);


        var (identity, token) = await sut.Execute(new SignInCommand("Test", "qwerty"), CancellationToken.None);
        encryptor.Verify(x => x.Encrypt("19860b88-35fd-4ea7-9527-a3b94645911c", It.IsAny<byte[]>(), It.IsAny<CancellationToken>()));
    }
}