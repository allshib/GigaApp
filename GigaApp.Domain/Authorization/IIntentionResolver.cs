using GigaApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Authorization
{
    public interface IIntentionResolver
    {
    }
    public interface IIntentionResolver<in TIntention> : IIntentionResolver
    {
        bool IsAllowed(IIdentity subject, TIntention intention);
    }
}
