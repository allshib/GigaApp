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

namespace GigaApp.Domain.DependencyInjection
{
    public static class ServiceCollectionEx
    {
        public static IServiceCollection AddForumDomain(this IServiceCollection services) {
            services
                .AddScoped<IGetForumsUseCase, GetForumUseCase>()
                .AddScoped<ICreateTopicUseCase, CreateTopicUseCase>()
                .AddScoped<IGetTopicsUseCase, GetTopicsUseCase>()
                .AddScoped<ICreateForumUseCase, CreateForumUseCase>();

            services
                .AddScoped<IIntentionResolver, TopicIntetntionResolver>()
                .AddScoped<IIntentionResolver, ForumIntentionResolver>()
                .AddScoped<IIntentionManager, IntentionManager>()
                .AddScoped<IIdentityProvider, IdentityProvider>();

            services
                .AddScoped<IGuidFactory, GuidFactory>()
                .AddScoped<IMomentProvider, MomentProvider>()
                .AddValidatorsFromAssemblyContaining<GigaApp.Domain.Models.Forum>(includeInternalTypes: true);


            return services;
        }
    }
}
