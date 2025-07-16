using GigaApp.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using Testcontainers.MsSql;

namespace GigaApp.E2E
{
    public class ForumApiApplicationFactory: WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer msSqlContainer = new MsSqlBuilder().Build();

        public ForumDbContext forumDbContext { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["ConnectionStrings:MsSql"] = msSqlContainer.GetConnectionString(),
                    ["Authentication:Base64Key"] = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32))
                })
                .Build();

            builder.UseConfiguration(config);

            base.ConfigureWebHost(builder);
        }

        public async Task InitializeAsync()
        {
            await msSqlContainer.StartAsync();
            forumDbContext = new ForumDbContext(new DbContextOptionsBuilder<ForumDbContext>()
                .UseSqlServer(msSqlContainer.GetConnectionString()).Options);

            forumDbContext.Database.Migrate();
        }

        public new async Task DisposeAsync() => await msSqlContainer.DisposeAsync();
        
    }
}
