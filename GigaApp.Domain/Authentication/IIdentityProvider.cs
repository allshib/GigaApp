using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Identity
{
    /// <summary>
    /// Провайдер авторизации
    /// Содержит текущего пользователя
    /// </summary>
    public interface IIdentityProvider
    {
        IIdentity Current { get; }
    }
}
