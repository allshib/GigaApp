using GigaApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Authentication
{
    internal class IdentityProvider : IIdentityProvider
    {
        public IIdentity Current => new User(Guid.Parse("ca4659c5-b98f-4c54-b746-dde6a98d9674"));
    }
}
