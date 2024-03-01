using AutoMapper;
using AutoMapper.QueryableExtensions;
using GigaApp.Domain.Authentication;
using GigaApp.Domain.UseCases.SignIn;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigaApp.Domain.UseCases;

namespace GigaApp.Storage.Storages
{
    internal class SignInStorage : ISignInStorage
    {
        private readonly ForumDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IGuidFactory guidFactory;

        public SignInStorage(
            ForumDbContext dbContext,
            IGuidFactory guidFactory,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.guidFactory = guidFactory;
        }

        public async Task<Guid> CreateSession(Guid userId, DateTimeOffset expirationMoment, CancellationToken cancellationToken)
        {
            var session = new Session
            {
                SessionId = guidFactory.Create(),
                UserId = userId,
                ExpiresAt = expirationMoment
            };
            await dbContext.AddAsync(session);
            await dbContext.SaveChangesAsync(cancellationToken);

            return session.SessionId;
        }

        public async Task<RecognizedUser?> FindUser(string login, CancellationToken cancellationToken)
        {
            return await dbContext.Users
                .Where(u => u.Login.Equals(login))
                .ProjectTo<RecognizedUser>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
