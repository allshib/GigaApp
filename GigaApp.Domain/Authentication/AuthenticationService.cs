using GigaApp.Domain.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace GigaApp.Domain.Authentication
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly ISymmetricDecryptor decryptor;
        private readonly IAuthenticationStorage storage;
        private readonly ILogger<AuthenticationService> logger;
        private readonly AuthenticationConfiguration authConfig;

        public AuthenticationService(
            ISymmetricDecryptor decryptor,
            IOptions<AuthenticationConfiguration> authOptions,
            IAuthenticationStorage storage,
            ILogger<AuthenticationService> logger)
        {
            this.decryptor = decryptor;
            this.storage = storage;
            this.logger = logger;
            authConfig = authOptions.Value;
        }

        public async Task<IIdentity> Authenticate(string authToken, CancellationToken cancellationToken)
        {
            string sessionIdString;
            try
            {
                sessionIdString = await decryptor.Decrypt(authToken, authConfig.Key, cancellationToken);
            }
            catch (CryptographicException ex)
            {
                logger.LogWarning(ex, "Cannot decrypt auth token : {AuthToken}", authToken);
                return User.Guest;
            }
            
            if(!Guid.TryParse(sessionIdString, out var sessionId))
            {
                return User.Guest;
            }
            var session = await storage.FindSession(sessionId, cancellationToken);

            if(session is null)
            {
                return User.Guest;
            }

            if(session.ExpiresAt < DateTimeOffset.UtcNow)
            {
                return User.Guest;
            }
 
            return new User(session.UserId, session.Id);
        }
    }
}
