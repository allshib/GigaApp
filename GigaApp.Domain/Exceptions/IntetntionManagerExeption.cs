using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Exceptions
{
    public class IntetntionManagerExeption : Exception
    {

        public IntetntionManagerExeption() : base("Action is not allowed") { }
    }
}
