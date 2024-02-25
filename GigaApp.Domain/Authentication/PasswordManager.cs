using System.Security.Cryptography;
using System.Text;

namespace GigaApp.Domain.Authentication
{
    public class PasswordManager : IPasswordManager
    {
        const int saltLenght = 100;
        private readonly Lazy<SHA256> sha256 = new(SHA256.Create);

        public bool ComparePasswords(string password, byte[] salt, byte[] hash)
        => ComputeHash(password, salt).SequenceEqual(hash);


        public (byte[] Salt, byte[] Hash) GeneratePasswordParts(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(saltLenght);


            var hash = ComputeHash(password, salt);

            lock (sha256)
            {
                return (salt, hash.ToArray());
            }
        }


        /// <summary>
        /// Возвращает массив байт (посчитанный Hash из пароля и соли)
        /// ReadOnlySpan можно эффективно сравнивать
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private ReadOnlySpan<byte> ComputeHash(string password, byte[] salt)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            var buffer = new byte[passwordBytes.Length + salt.Length];

            //Копирую в буфер passwordBytes
            Array.Copy(passwordBytes, buffer, passwordBytes.Length);
            //Копирую в соль в конец passwordBytes
            Array.Copy(salt, 0, buffer,passwordBytes.Length, salt.Length);

            return sha256.Value.ComputeHash(buffer);
        }
    }
}
