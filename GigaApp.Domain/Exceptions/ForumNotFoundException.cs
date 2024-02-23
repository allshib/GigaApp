using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Exceptions;

public class ForumNotFoundException: DomainException
{
    public ForumNotFoundException(Guid forumId) : base(ErrorCode.Gone, $"Forum with id {forumId} is not found!") { }
}

