﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.UseCases.SignOut
{
    public interface ISignOutStorage
    {
        Task RemoveSession(Guid sessionId, CancellationToken cancellationToken);
    }
}
