using FluentValidation;
using FluentValidation.Results;
using GigaApp.Domain.Authentication;
using GigaApp.Domain.Exceptions;
using GigaApp.Domain.Identity;
using MediatR;
using Microsoft.Extensions.Options;

namespace GigaApp.Domain.UseCases.SignIn
{
    internal class SignInUseCase : IRequestHandler<SignInCommand, (IIdentity identity, string token)>

    {
    private readonly IValidator<SignInCommand> validator;
    private readonly ISignInStorage signInStorage;
    private readonly IPasswordManager passwordManager;
    private readonly ISymmetricEncryptor encryptor;
    private readonly AuthenticationConfiguration authenticationConfiguration;

    public SignInUseCase(
        IValidator<SignInCommand> validator,
        ISignInStorage signInStorage,
        IPasswordManager passwordManager,
        ISymmetricEncryptor encryptor,
        IOptions<AuthenticationConfiguration> options
    )
    {
        this.validator = validator;
        this.signInStorage = signInStorage;
        this.passwordManager = passwordManager;
        this.encryptor = encryptor;
        authenticationConfiguration = options.Value;
    }


        public async Task<(IIdentity identity, string token)> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var recognizedUser = await signInStorage.FindUser(request.Login, cancellationToken);

            if (recognizedUser is null)
            {
                throw new ValidationException(new ValidationFailure[]
                {
                    new()
                    {
                        PropertyName = nameof(request.Login),
                        ErrorCode = ValidationErrorCode.Invalid,
                        AttemptedValue = request.Login
                    }
                });
            }

            var passwordMatches = passwordManager
                .ComparePasswords(request.Password, recognizedUser.Salt, recognizedUser.PasswordHash);

            if (!passwordMatches)
            {
                throw new ValidationException(new ValidationFailure[]
                {
                    new()
                    {
                        PropertyName = nameof(request.Password),
                        ErrorCode = ValidationErrorCode.Invalid,
                        AttemptedValue = request.Password
                    }
                });
            }

            var sessionId = await signInStorage.CreateSession(recognizedUser.UserId,
                DateTimeOffset.UtcNow + TimeSpan.FromHours(1), cancellationToken);

            var token = await encryptor.Encrypt(sessionId.ToString(), authenticationConfiguration.Key, cancellationToken);

            return (new User(recognizedUser.UserId, sessionId), token);
        }
    }

}
