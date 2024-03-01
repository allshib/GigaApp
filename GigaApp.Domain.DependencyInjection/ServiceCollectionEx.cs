using GigaApp.Domain.Authentication;
using GigaApp.Domain.Authorization;
using GigaApp.Domain.Identity;
using GigaApp.Domain.UseCases.CreateTopic;
using GigaApp.Domain.UseCases.GetForums;
using GigaApp.Domain.UseCases;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using GigaApp.Domain.UseCases.GetTopics;
using GigaApp.Domain.UseCases.CreateForum;
using GigaApp.Domain.UseCases.SignOn;
using GigaApp.Domain.UseCases.SignIn;
using GigaApp.Domain.UseCases.SignOut;

namespace GigaApp.Domain.DependencyInjection
{
    public static class ServiceCollectionEx
    {
        public static IServiceCollection AddForumDomain(this IServiceCollection services)
        {
            services
                .AddScoped<IGetForumsUseCase, GetForumUseCase>()
                .AddScoped<ICreateTopicUseCase, CreateTopicUseCase>()
                .AddScoped<IGetTopicsUseCase, GetTopicsUseCase>()
                .AddScoped<ICreateForumUseCase, CreateForumUseCase>()
                .AddScoped<ISignOnUseCase, SignOnUseCase>()
                .AddScoped<ISignInUseCase, SignInUseCase>()
                .AddScoped<ISignOutUseCase, SignOutUseCase>();

            services
                .AddScoped<IIntentionResolver, TopicIntetntionResolver>()
                .AddScoped<IIntentionResolver, ForumIntentionResolver>()
                .AddScoped<IIntentionManager, IntentionManager>()
                .AddScoped<IIdentityProvider, IdentityProvider>()
                .AddScoped<IPasswordManager,  PasswordManager>()
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<ISymmetricDecryptor, AesSymmetricEncryptorDecryptor>()
                .AddScoped<ISymmetricEncryptor, AesSymmetricEncryptorDecryptor>();

            services
                .AddScoped<IGuidFactory, GuidFactory>()
                .AddScoped<IMomentProvider, MomentProvider>()
                .AddValidatorsFromAssemblyContaining<GigaApp.Domain.Models.Forum>(includeInternalTypes: true);


            return services;
        }
    }
}
