using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;

namespace GigaApp.Storage.Tests
{
    public class StorageTestFixture : IAsyncLifetime
    {
        private readonly MsSqlContainer msSqlContainer = new MsSqlBuilder().Build();


        public ForumDbContext GetDbContext()
        {
            return new ForumDbContext(new DbContextOptionsBuilder<ForumDbContext>()
                .UseSqlServer(msSqlContainer.GetConnectionString()).Options);
        }
        

        public async Task InitializeAsync()
        {
            await msSqlContainer.StartAsync();
            var forumDbContext = new ForumDbContext(new DbContextOptionsBuilder<ForumDbContext>()
                .UseSqlServer(msSqlContainer.GetConnectionString()).Options);

            forumDbContext.Database.Migrate();
        }

        public new async Task DisposeAsync() => await msSqlContainer.DisposeAsync();
    }
}
