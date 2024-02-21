using GigaApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Authorization
{
    /// <summary>
    /// Интерфейс разрешения, который может быть реализован через что угодно
    /// </summary>
    public interface IIntentionResolver
    {
    }
    /// <summary>
    /// Интерфейс разрешения, реализуемый через Enum
    /// </summary>
    /// <typeparam name="TIntention"></typeparam>
    public interface IIntentionResolver<in TIntention> : IIntentionResolver
    {
        bool IsAllowed(IIdentity subject, TIntention intention);
    }
}
