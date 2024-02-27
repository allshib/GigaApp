﻿using FluentValidation;
using FluentValidation.Results;
using GigaApp.Domain.Authentication;
using GigaApp.Domain.Exceptions;
using GigaApp.Domain.Identity;
using Microsoft.Extensions.Options;

namespace GigaApp.Domain.UseCases.SignIn
{
    internal class SignInUseCase : ISignInUseCase
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
        public async Task<(IIdentity identity, string token)> Execute(SignInCommand command, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            var recognizedUser = await signInStorage.FindUser(command.Login, cancellationToken);

            if(recognizedUser is null)
            {
                throw new ValidationException(new ValidationFailure[]
                {
                    new()
                    {
                        PropertyName = nameof(command.Login), 
                        ErrorCode = ValidationErrorCode.Invalid, 
                        AttemptedValue = command.Login
                    }
                });
            }
            var passwordMatches = passwordManager
                .ComparePasswords(command.Password, recognizedUser.Salt, recognizedUser.PasswordHash);

            if (!passwordMatches)
            {
                throw new ValidationException(new ValidationFailure[]
                {
                    new()
                    {
                        PropertyName = nameof(command.Password),
                        ErrorCode = ValidationErrorCode.Invalid,
                        AttemptedValue = command.Password
                    }
                });
            }


            var token = await encryptor.Encrypt(recognizedUser.UserId.ToString(), authenticationConfiguration.Key, cancellationToken);

            return (new User(recognizedUser.UserId), token);
        }
    }

}
