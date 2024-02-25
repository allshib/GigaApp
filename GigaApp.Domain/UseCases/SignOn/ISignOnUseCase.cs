using GigaApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.UseCases.SignOn
{
    public interface ISignOnUseCase
    {

        Task<IIdentity> Execute (SignOnCommand command, CancellationToken cancellationToken);
    }
}
