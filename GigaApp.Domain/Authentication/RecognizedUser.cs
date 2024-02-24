using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Authentication
{
    public class RecognizedUser
    {
        public required Guid UserId { get; set; }
        public required string Salt {  get; set; }
        public required string PasswordHash { get; set; }
    }
}
