using GigaApp.Domain.Identity;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace GigaApp.Domain.Authentication
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly ISymmetricDecryptor decryptor;
        private readonly IPasswordManager securityManager;
        private readonly AuthenticationConfiguration authConfig;
        private readonly Lazy<Aes> aesService = new(Aes.Create);

        public AuthenticationService(
            ISymmetricDecryptor decryptor,
            IPasswordManager securityManager, 
            IOptions<AuthenticationConfiguration> authOptions)
        {
            this.decryptor = decryptor;
            this.securityManager = securityManager;
            authConfig = authOptions.Value;
        }

        public async Task<IIdentity> Authenticate(string authToken, CancellationToken cancellationToken)
        {
            var userIdString = await decryptor.Decrypt(authToken, authConfig.Key, cancellationToken);
            //TODO: верифицировать UserId
            return new User(Guid.Parse(userIdString));
        }
    }
}
