using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Authentication
{
    public class AuthenticationConfiguration
    {
        public string Base64Key { get; set; }

        public byte[] Key => Convert.FromBase64String(Base64Key);
    }
}
