using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Authentication
{
    public class Session
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }

    }
}
