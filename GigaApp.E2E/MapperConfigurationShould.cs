using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.E2E
{
    public class MapperConfigurationShould : IClassFixture<ForumApiApplicationFactory>
    {
        private readonly ForumApiApplicationFactory factory;

        public MapperConfigurationShould(ForumApiApplicationFactory factory)
        {
            this.factory = factory;
        }
        [Fact]
        public void BeValid()
        {
            factory.Services
                .GetRequiredService<IMapper>()
                .ConfigurationProvider.Invoking(x => x.AssertConfigurationIsValid())
                .Should().NotThrow();
        }
    }
}
