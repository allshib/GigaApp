using GigaApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.UseCases.SignIn
{
    public interface ISignInUseCase
    {
        Task<(IIdentity identity, string token)> Execute(SignInCommand command, CancellationToken cancellationToken);
    }

}
