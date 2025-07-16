using FluentAssertions;
using GigaApp.Domain.UseCases;
using GigaApp.Storage.Storages;
using Microsoft.EntityFrameworkCore;
namespace GigaApp.Storage.Tests;

public class CreateForumStorageShould : IClassFixture<StorageTestFixture>
{
    private readonly StorageTestFixture fixture;

    private readonly CreateForumStorage sut;

    public CreateForumStorageShould(StorageTestFixture fixture)
    {
        this.fixture = fixture;
        sut = new CreateForumStorage(fixture.GetMemoryCache(),new GuidFactory(), fixture.GetDbContext(), fixture.GetMapper());
    }


    [Fact]
    public async Task InsertNewForumInDataBase()
    {
        var forum = await sut.Create("Test", CancellationToken.None);

        forum.Id.Should().NotBeEmpty();

        await using var dBContext = fixture.GetDbContext();

        var forumTitles = await dBContext.Forums
            .Where(x => x.ForumId == forum.Id).Select(f=>f.Title)
            .ToArrayAsync();

        forumTitles.Should().Contain("Test");
    }
}