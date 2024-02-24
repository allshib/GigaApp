using GigaApp.Domain.Identity;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace GigaApp.Domain.Authentication
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationStorage storage;
        private readonly ISecurityManager securityManager;
        private readonly AuthenticationConfiguration authConfig;
        private readonly Lazy<TripleDES> tripleDESService = new(TripleDES.Create);

        public AuthenticationService(
            IAuthenticationStorage storage,
            ISecurityManager securityManager, 
            IOptions<AuthenticationConfiguration> authOptions)
        {
            this.storage = storage;
            this.securityManager = securityManager;
            this.authConfig = authOptions.Value;
        }

        public async Task<(bool success, string authToken)> SignIn(BasicSignInCredentials credentials, CancellationToken cancellationToken)
        {
            var recognizedUser =  await storage.FindUser(credentials.Login, cancellationToken);

            if(recognizedUser is null)
            {
                throw new Exception("User not Found");
            }

            var success = securityManager.ComparePasswords(credentials.Password, recognizedUser.Salt, recognizedUser.PasswordHash);
            var userId = recognizedUser.UserId.ToByteArray();

            using var encryptedStream = new MemoryStream();
            var key = Convert.FromBase64String(authConfig.Key);
            var iv = Convert.FromBase64String(authConfig.Iv);
            await using (var stream = new CryptoStream(
                encryptedStream,
                tripleDESService.Value.CreateEncryptor(key, iv),
                CryptoStreamMode.Write))
            {
                await stream.WriteAsync(userId, cancellationToken);
            }

                return (success, Convert.ToBase64String(encryptedStream.ToArray()));
        }

        public async Task<IIdentity> Authenticate(string authToken, CancellationToken cancellationToken)
        {
            using var decryptedStream = new MemoryStream();
            var key = Convert.FromBase64String(authConfig.Key);
            var iv = Convert.FromBase64String(authConfig.Iv);

            await using (var stream = new CryptoStream(
                decryptedStream,
                tripleDESService.Value.CreateDecryptor(key, iv),
                CryptoStreamMode.Write))
            {
                var encryptedBytes = Convert.FromBase64String(authToken);
                await stream.WriteAsync(encryptedBytes, cancellationToken);
            }


            var userId = new Guid(decryptedStream.ToArray());

            return new User(userId);
        }
    }
}
