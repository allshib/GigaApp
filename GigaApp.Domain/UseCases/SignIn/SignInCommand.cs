using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.UseCases.SignIn
{
    public record SignInCommand(string Login, string Password);
    
}
