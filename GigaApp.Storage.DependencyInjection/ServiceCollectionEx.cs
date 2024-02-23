using GigaApp.Domain.UseCases.CreateTopic;
using GigaApp.Domain.UseCases.GetForums;
using GigaApp.Domain.UseCases.GetTopics;
using GigaApp.Storage.Storages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GigaApp.Storage.DependencyInjection
{
    public static class ServiceCollectionEx
    {
        public static IServiceCollection AddForumStorage(this IServiceCollection services, string connectionString) => services
            .AddScoped<ICreateTopicStorage, CreateTopicStorage>()
            .AddScoped<IGetForumsStorage, GetForumsStorage>()
            .AddScoped<IGetTopicsStorage,  GetTopicsStorage>()
            .AddDbContext<ForumDbContext>(options =>options.UseSqlServer(connectionString, b => 
                b.MigrationsAssembly("GigaApp.API")), ServiceLifetime.Singleton)
            .AddMemoryCache();

    }
}
