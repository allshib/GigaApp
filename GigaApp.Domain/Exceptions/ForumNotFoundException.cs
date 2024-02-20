﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Exceptions
{
    public class ForumNotFoundException: Exception
    {
        public ForumNotFoundException(Guid forumId) : base($"Forum with id {forumId} is not found!") { }
    }
}