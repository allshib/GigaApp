using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Authentication
{
    public class RecognizedUser
    {
        public Guid UserId { get; set; }
        public byte[] Salt {  get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
