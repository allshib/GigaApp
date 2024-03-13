﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GigaApp.Domain.Authentication;
using GigaApp.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace GigaApp.Storage.Storages
{
    internal class AuthenticationStorage : IAuthenticationStorage
    {
        private readonly ForumDbContext dbContext;
        private readonly IMapper mapper;
        public AuthenticationStorage(ForumDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<Domain.Authentication.Session?> FindSession(Guid sessionId, CancellationToken cancellationToken)
        {
            var session = await dbContext.Sessions
                .SingleOrDefaultAsync(s => s.SessionId == sessionId, cancellationToken);

            return mapper.Map<Domain.Authentication.Session>(session);
        }
    }
}
