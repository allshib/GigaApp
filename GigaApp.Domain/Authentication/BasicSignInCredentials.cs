using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Authentication
{
    public record BasicSignInCredentials(string Login, string Password);
}
