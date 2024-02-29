using GigaApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Authentication
{
    public class User : IIdentity
    {
        public User(Guid userId, Guid sessionId)
        {
            UserId = userId;
            SessionId = sessionId;
        }
        public Guid UserId { get; }

        public static User Guest => new(Guid.Empty, Guid.Empty);

        public Guid SessionId { get; }
    }
}
