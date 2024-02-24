using AutoMapper;
using AutoMapper.QueryableExtensions;
using GigaApp.Domain.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Storage.Storages
{
    internal class AuthenticationStorage : IAuthenticationStorage
    {
        private readonly ForumDbContext dbContext;
        private readonly IMapper mapper;

        public AuthenticationStorage(
            ForumDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
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
