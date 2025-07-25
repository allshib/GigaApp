﻿using GigaApp.Domain.UseCases;
using GigaApp.Domain.UseCases.SignOn;
using GigaApp.Storage.Entities;

namespace GigaApp.Storage.Storages
{
    internal class SignOnStorage : ISignOnStorage
    {
        private readonly ForumDbContext dbContext;
        private readonly IGuidFactory guidFactory;

        public SignOnStorage(
            ForumDbContext dbContext,
            IGuidFactory guidFactory
            )
        {
            this.dbContext = dbContext;
            this.guidFactory = guidFactory;
        }
        public async Task<Guid> CreateUser(string login, byte[] salt, byte[] hash, CancellationToken cancellationToken)
        {
            var userId = guidFactory.Create();
            await dbContext.AddAsync(new User
            {
                UserId = userId,
                Login = login,
                Salt = salt,
                PasswordHash = hash
            });
            await dbContext.SaveChangesAsync();

            return userId;
        }
    }
}
