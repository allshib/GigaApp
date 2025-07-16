using GigaApp.Domain.Authentication;
using GigaApp.Domain.Identity;
using MediatR;

namespace GigaApp.Domain.UseCases.SignOn
{
    internal class SignOnUseCase(
        ISignOnStorage   signOnStorage,
        IPasswordManager passwordManager): IRequestHandler<SignOnCommand, IIdentity>
    {
        public async Task<IIdentity> Handle(SignOnCommand request, CancellationToken cancellationToken)
        {
            var (salt, hash) = passwordManager.GeneratePasswordParts(request.Password);
            var userId = await signOnStorage.CreateUser(request.Login, salt, hash, cancellationToken);

            return new User(userId, Guid.Empty);
        }
    }
}
