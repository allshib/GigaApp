using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigaApp.Domain.UseCases.SignIn;

namespace GigaApp.Domain.Authentication
{
    public interface IAuthenticationStorage
    {
        Task<RecognizedUser?> FindUser(string login, CancellationToken cancellationToken);
    }
}
