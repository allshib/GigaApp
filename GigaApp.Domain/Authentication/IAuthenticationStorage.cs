using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Authentication
{
    public interface IAuthenticationStorage
    {
        Task<RecognizedUser?> FindUser(string login, CancellationToken cancellationToken);
    }
}
