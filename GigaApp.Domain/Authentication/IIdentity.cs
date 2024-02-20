using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Identity
{
    public interface IIdentity
    {
        Guid UserId { get; }
    }

    public static class IdentityEx
    {
        public static bool IsAuthenticated(this IIdentity identity) => identity.UserId != Guid.Empty;
    }
}
