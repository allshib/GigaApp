using FluentAssertions;
using GigaApp.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.E2E
{
    public class TopicEndpointShould : IClassFixture<ForumApiApplicationFactory>, IAsyncLifetime
    {
        private readonly ForumApiApplicationFactory factory;
        //private readonly Guid forumId = Guid.Parse("756df757-cf72-4ef5-b87d-f8f8f0d35cfa");
        //private ForumDbContext dbContext;

        public TopicEndpointShould(ForumApiApplicationFactory factory)
        {
            this.factory = factory;
        }
        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public  Task InitializeAsync()
        {
            return Task.CompletedTask;
        }



        [Fact]
        public async Task ReturnForbidden_WhenNotAuthenticated()
        {
            using var hhtpClient = factory.CreateClient();

            using var responseForum = await hhtpClient.PostAsync(
                "forum",
                JsonContent.Create(new { title = "TestForum" }));
            //responseForum.EnsureSuccessStatusCode();

            var forum = await responseForum.Content.ReadFromJsonAsync<GigaApp.API.Models.Forum>();
            forum.Should().NotBeNull();

            using var responseTopic = await hhtpClient.PostAsync($"forum/{forum!.Id}/topics", JsonContent.Create(new { title = "HelloWorld" }));

            responseTopic.StatusCode.Should().Be(System.Net.HttpStatusCode.Forbidden);
        }
    }
}
