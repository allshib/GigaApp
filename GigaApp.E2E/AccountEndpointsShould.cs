using System.Net.Http.Json;
using FluentAssertions;
using GigaApp.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using User = GigaApp.Domain.Authentication.User;


namespace GigaApp.E2E;

public class AccountEndpointsShould(ForumApiApplicationFactory factory) : IClassFixture<ForumApiApplicationFactory>
{
    [Fact]
    public async Task SignInAfterSignOn()
    {
        using var httpClient = factory.CreateClient();

        // Given I create a new account
        // | Login | Password |
        // | Test  | qwerty   |
        using var signOnResponse = await httpClient.PostAsync(
            "account", JsonContent.Create(new { login = "Test", password = "qwerty" }));
        signOnResponse.IsSuccessStatusCode.Should().BeTrue();
        //var createdUsers = await signOnResponse.Content.ReadAsStringAsync();

        var createdUser = await signOnResponse.Content.ReadFromJsonAsync<User>();

        using var signInResponse = await httpClient.PostAsync(
            "account/signin", JsonContent.Create(new { login = "Test", password = "qwerty" }));
        signInResponse.IsSuccessStatusCode.Should().BeTrue();

        var signedInUser = await signInResponse.Content.ReadFromJsonAsync<User>();
        signedInUser!.UserId.Should().Be(createdUser!.UserId);



        var createForumResponse = await httpClient.PostAsync(
            "forum", JsonContent.Create(new { title = "Test title" }));
        createForumResponse.IsSuccessStatusCode.Should().BeTrue();
        var forum = (await createForumResponse.Content.ReadFromJsonAsync<GigaApp.API.Models.Forum>())!;

        var createTopicResponse = await httpClient.PostAsync(
            $"forum/{forum.Id}/topics", JsonContent.Create(new { title = "New topic" }));
        createTopicResponse.IsSuccessStatusCode.Should().BeTrue();

        await using var scope = factory.Services.CreateAsyncScope();
        var domainEvents = await scope.ServiceProvider.GetRequiredService<ForumDbContext>()
            .DomainEvents.ToArrayAsync();
        domainEvents.Should().HaveCount(1);
    }
}