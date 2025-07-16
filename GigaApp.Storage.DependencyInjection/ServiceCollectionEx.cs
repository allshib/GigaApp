using GigaApp.Domain.UseCases.CreateForum;
using GigaApp.Domain.UseCases.CreateTopic;
using GigaApp.Domain.UseCases.GetForums;
using GigaApp.Domain.UseCases.GetTopics;
using GigaApp.Domain.UseCases.SignIn;
using GigaApp.Domain.UseCases.SignOn;
using GigaApp.Storage.Storages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using GigaApp.Domain.Authentication;
using GigaApp.Domain.UseCases.SignOut;
using GigaApp.Domain;
using GigaApp.Domain.UseCases;
using GigaApp.Domain.UseCases.GetForumByKey;
using GigaApp.Domain.UseCases.DeleteForumByKey;
using GigaApp.Domain.UseCases.UpdateForumUseCase;

namespace GigaApp.Storage.DependencyInjection
{
    public static class ServiceCollectionEx
    {
        public static IServiceCollection AddForumStorage(this IServiceCollection services, string connectionString)
        {
            services
                .AddScoped<ICreateTopicStorage, CreateTopicStorage>()
                .AddScoped<IGetForumsStorage, GetForumsStorage>()
                .AddScoped<IGetTopicsStorage, GetTopicsStorage>()
                .AddScoped<ICreateForumStorage, CreateForumStorage>()
                .AddScoped<ISignInStorage, SignInStorage>()
                .AddScoped<ISignOnStorage, SignOnStorage>()
                .AddScoped<ISignOutStorage, SignOutStorage>()
                .AddScoped<IAuthenticationStorage, AuthenticationStorage>()
                .AddScoped<IDomainEventStorage, DomainEventStorage>()
                .AddScoped<IGetForumByKeyStorage, GetForumByKeyStorage>()
                .AddScoped<IDeleteForumByKeyStorage, DeleteForumByKeyStorage>()
                .AddScoped<IUpdateForumStorage, UpdateForumStorage>();

            services.AddDbContext<ForumDbContext>(options => options.UseSqlServer(connectionString, b =>
                b.MigrationsAssembly("GigaApp.API")), ServiceLifetime.Singleton);

            services.AddMemoryCache();

            services.AddAutoMapper(config =>config.AddMaps(Assembly.GetAssembly(typeof(ForumDbContext))));

            services.AddSingleton<IUnitOfWork>(sp => new UnitOfWork(sp));

            return services;
        }

    }
}
