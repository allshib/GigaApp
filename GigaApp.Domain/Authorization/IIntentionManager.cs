using GigaApp.Domain.Exceptions;
using GigaApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Authorization
{
    /// <summary>
    /// Менеджер, проверяющий разрешения на доступ к чему-либо в системе
    /// Разрешения основаны на атрибутах, а не ролях
    /// </summary>
    public interface IIntentionManager
    {
        bool IsAllowed<TIntention>(TIntention intention) where TIntention : Enum;
        bool IsAllowed<TIntention, TObject>(TIntention intention, TObject target) where TIntention : Enum;
    }

    public class IntentionManager : IIntentionManager
    {
        /// <summary>
        /// Список разрешений
        /// </summary>
        private readonly IEnumerable<IIntentionResolver> resolvers;
        private readonly IdentityProvider identityProvider;

        public IntentionManager(IEnumerable<IIntentionResolver> resolvers, IdentityProvider identityProvider)
        {
            this.resolvers = resolvers;
            this.identityProvider = identityProvider;
        }

        public bool IsAllowed<TIntention>(TIntention intention) where TIntention : Enum
        {
            var matchResolver = resolvers.OfType<IIntentionResolver<TIntention>>().FirstOrDefault();
            return matchResolver?.IsAllowed(identityProvider.Current, intention)?? false;
        }

        public bool IsAllowed<TIntention, TObject>(TIntention intention, TObject target) where TIntention : Enum
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Временный класс бросающий ошибку, если нет разрешения
    /// </summary>
    public static class IntetntionManagerExtensions
    {
        public static void ThrowIfForbidden<TIntetntion>(this IIntentionManager intetManager, TIntetntion intetntion) 
            where TIntetntion : Enum
        {
            if (!intetManager.IsAllowed(intetntion))
                throw new IntetntionManagerExeption();
        }
    }
}