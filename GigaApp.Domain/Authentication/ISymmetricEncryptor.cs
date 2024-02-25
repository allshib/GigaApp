using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Authentication
{
    internal interface ISymmetricEncryptor
    {
        Task<string> Encrypt(string plainText, byte[] key, CancellationToken cancellationToken);
    }
}
