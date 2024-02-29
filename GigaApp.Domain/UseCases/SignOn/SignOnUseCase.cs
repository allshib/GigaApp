using FluentValidation;
using GigaApp.Domain.Authentication;
using GigaApp.Domain.Identity;

namespace GigaApp.Domain.UseCases.SignOn
{
    internal class SignOnUseCase : ISignOnUseCase
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

        public async Task<IIdentity> Execute(SignOnCommand command, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            var (salt, hash) = passwordManager.GeneratePasswordParts(command.Password);
            var userId = await signOnStorage.CreateUser(command.Login, salt, hash, cancellationToken);

            return new User(userId,Guid.Empty);
        }
    }
}
