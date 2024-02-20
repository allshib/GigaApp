using GigaApp.Domain.Exceptions;
using GigaApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Authorization
{
    public interface IIntentionManager
    {
        bool IsAllowed<TIntention>(TIntention intention) where TIntention : struct;
        bool IsAllowed<TIntention, TObject>(TIntention intention, TObject target) where TIntention : struct;
    }
    public class IntentionManager : IIntentionManager
    {
        private readonly IEnumerable<IIntentionResolver> resolvers;
        private readonly IdentityProvider identityProvider;

        public IntentionManager(IEnumerable<IIntentionResolver> resolvers, IdentityProvider identityProvider)
        {
            this.resolvers = resolvers;
            this.identityProvider = identityProvider;
        }

        public bool IsAllowed<TIntention>(TIntention intention) where TIntention : struct
        {
            var matchResolver = resolvers.OfType<IIntentionResolver<TIntention>>().FirstOrDefault();
            return matchResolver?.IsAllowed(identityProvider.Current, intention)?? false;
        }

        public bool IsAllowed<TIntention, TObject>(TIntention intention, TObject target) where TIntention : struct
        {
            throw new NotImplementedException();
        }
    }

    public static class IntetntionManagerExtensions
    {
        public static void ThrowIfForbidden<TIntetntion>(this IIntentionManager intetManager, TIntetntion intetntion) 
            where TIntetntion : struct
        {
            if (!intetManager.IsAllowed(intetntion))
                throw new IntetntionManagerExeption();
        }
    }
}