using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigaApp.Domain.Authentication
{
    internal interface IPasswordManager
    {
        bool ComparePasswords(string password, byte[] salt, byte[] hash);

        (byte[] Salt, byte[] Hash) GeneratePasswordParts(string password);
    }
}
