using GigaApp.Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.UseCases.SignIn
{
    public interface ISignInStorage
    {
        Task<RecognizedUser> FindUser(string login, CancellationToken cancellationToken);
    }
}
