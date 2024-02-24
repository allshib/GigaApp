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
        Task<(bool success, string authToken)> SignIn(BasicSignInCredentials credentials, CancellationToken cancellationToken);

        Task<IIdentity> Authenticate(string authToken, CancellationToken cancellationToken);
    }
}
