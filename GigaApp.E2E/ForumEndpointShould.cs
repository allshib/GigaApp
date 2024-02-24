using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using GigaApp.API.Models;

namespace GigaApp.E2E
{
    public class ForumEndpointShould : IClassFixture<ForumApiApplicationFactory>
    {
        private readonly ForumApiApplicationFactory factory;

        public ForumEndpointShould(ForumApiApplicationFactory factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task ReturnNewForum()
        {
            using var hhtpClient = factory.CreateClient();

            using var getinitForumResponse = await hhtpClient.GetAsync("forum");

            var initialForums = await getinitForumResponse.Content.ReadFromJsonAsync<Forum[]>();

            initialForums
                .Should().NotBeNull()
                .And.Subject.As<Forum[]>()
                .Should().BeEmpty();



            using var response = await hhtpClient.PostAsync(
                "forum", 
                JsonContent.Create(new {title = "Test"}));

            response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();


            var forum = await response.Content.ReadFromJsonAsync<Forum>();

            forum
                .Should().NotBeNull()
                .And.Subject.As<Forum>()
                .Title.Should().Be("Test");

            forum.Id.Should().NotBeEmpty();

            using var getForumResponse = await hhtpClient.GetAsync("forum");

            var forums = await getForumResponse.Content.ReadFromJsonAsync<Forum[]>();

            forums
                .Should().NotBeNull()
                .And.Subject.As<Forum[]>()
                .Should().Contain(f => f.Title == "Test");
        }


        [Fact]
        public async Task ReturnListOfForums()
        {
            using var hhtpClient = factory.CreateClient();

            using var response = await hhtpClient.GetAsync("forum");
            response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();


            var result = await response.Content.ReadAsStringAsync();
            result.Should().Be("[]");
        }
    }
}
