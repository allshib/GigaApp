using GigaApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Authentication
{
    public interface IAuthenticationService
    {
        Task<IIdentity> Authenticate(string authToken, CancellationToken cancellationToken);
    }
}
