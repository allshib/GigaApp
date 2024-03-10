﻿using FluentValidation;
using GigaApp.Domain.Authentication;
using GigaApp.Domain.Identity;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GigaApp.Domain.UseCases.SignOn
{
    internal class SignOnUseCase : IRequestHandler<SignOnCommand, IIdentity>
    {
        private readonly ISignOnStorage signOnStorage;
        private readonly IValidator<SignOnCommand> validator;
        private readonly IPasswordManager passwordManager;

        public SignOnUseCase(
            ISignOnStorage signOnStorage,
            IValidator<SignOnCommand> validator,
            IPasswordManager passwordManager)
        {
            this.signOnStorage = signOnStorage;
            this.validator = validator;
            this.passwordManager = passwordManager;
        }



        public async Task<IIdentity> Handle(SignOnCommand request, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var (salt, hash) = passwordManager.GeneratePasswordParts(request.Password);
            var userId = await signOnStorage.CreateUser(request.Login, salt, hash, cancellationToken);

            return new User(userId, Guid.Empty);
        }
    }
}
