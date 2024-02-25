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
        public User(Guid userId) {
            UserId = userId;
        }
        public Guid UserId { get; }

        public static User Guest => new(Guid.Empty);
    }
}
