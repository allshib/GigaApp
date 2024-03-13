﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GigaApp.Storage.Mapping;
using Testcontainers.MsSql;
using Microsoft.Extensions.Caching.Memory;
using GigaApp.Storage.Entities;

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

        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddMaps(Assembly.GetAssembly(typeof(ForumDbContext))));
            return new Mapper(config);
        }

        public IMemoryCache GetMemoryCache()
        {
            return new MemoryCache(new MemoryCacheOptions());
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
