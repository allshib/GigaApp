using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;


namespace GigaApp.E2E
{
    public class MapperConfigurationShould : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;

        public MapperConfigurationShould(WebApplicationFactory<Program> factory)
        {
            this.factory = factory;
        }
        [Fact]
        public void BeValid()
        {
            var config = factory.Services
                .GetRequiredService<IMapper>()
                .ConfigurationProvider;
            config.Invoking(x => x.AssertConfigurationIsValid())
            .Should().NotThrow();
        }
    }
}
