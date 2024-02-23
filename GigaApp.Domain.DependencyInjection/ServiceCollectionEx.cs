using GigaApp.Domain.Authentication;
using GigaApp.Domain.Authorization;
using GigaApp.Domain.Identity;
using GigaApp.Domain.UseCases.CreateTopic;
using GigaApp.Domain.UseCases.GetForums;
using GigaApp.Domain.UseCases;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace GigaApp.Domain.DependencyInjection
{
    public static class ServiceCollectionEx
    {
        public static IServiceCollection AddForumDomain(this IServiceCollection services) {
            services
                .AddScoped<IGetForumsUseCase, GetForumUseCase>()
                .AddScoped<ICreateTopicUseCase, CreateTopicUseCase>();

            services
                .AddScoped<IIntentionResolver, TopicIntetntionResolver>()
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
