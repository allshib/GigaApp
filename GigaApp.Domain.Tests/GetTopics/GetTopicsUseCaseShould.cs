using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using GigaApp.Domain.Exceptions;
using GigaApp.Domain.Models;
using GigaApp.Domain.UseCases.GetForums;
using GigaApp.Domain.UseCases.GetTopics;
using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Tests.GetTopics
{
    public class GetTopicsUseCaseShould
    {
        private readonly Mock<IGetTopicsStorage> storage;
        private readonly ISetup<IGetTopicsStorage, Task<(IEnumerable<Topic> topics, int totalCOunt)>> getTopicsSetup;
        private readonly ISetup<IGetForumsStorage, Task<IEnumerable<Forum>>> getForumStorageSetup;
        private readonly GetTopicsUseCase sut;

        public GetTopicsUseCaseShould()
        {
            var validator = new Mock<IValidator<GetTopicsQuery>>();
            validator.Setup(v => v.ValidateAsync(It.IsAny<GetTopicsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            storage = new Mock<IGetTopicsStorage>();
            getTopicsSetup =storage.Setup(v => v.GetTopics(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()));
            var getForumStorage = new Mock<IGetForumsStorage>();
            getForumStorageSetup = getForumStorage.Setup(v => v.GetForums(It.IsAny<CancellationToken>()));
            sut = new GetTopicsUseCase(validator.Object, getForumStorage.Object, storage.Object);
        }
        [Fact]
        public async Task ThrowForumNotFoundException_ExtractedNoForum()
        {
            var forumId = Guid.Parse("9d3511ef-afa7-4084-a587-e5a5a254742f");

            getForumStorageSetup.ReturnsAsync(new Forum[] { new Forum { Id = Guid.Parse("452e1f18-a036-4467-b577-b29e7fcbe724") } });

            await sut.Invoking(s => s.Handle(new GetTopicsQuery(forumId, 0, 1), CancellationToken.None))
                .Should().ThrowAsync<ForumNotFoundException>();
        }

        [Fact]
        public async Task ReturnTopics_ExtractedFromStorage()
        {
            var expectedResources = new Topic[] { new Topic() };

            var expectedTotalCount = 6;
            getTopicsSetup.ReturnsAsync((expectedResources, expectedTotalCount));

            var forumId = Guid.Parse("9d3511ef-afa7-4084-a587-e5a5a254742f");
            getForumStorageSetup.ReturnsAsync(new Forum[] { new Forum { Id = forumId } });
            var (actualResources, actualTotalCount) = await sut.Handle(new GetTopicsQuery(forumId, 5, 10), CancellationToken.None);

            actualResources.Should().BeEquivalentTo(expectedResources);
            actualTotalCount.Should().Be(expectedTotalCount);

            storage.Verify(s=>s.GetTopics(forumId, 5, 10, It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
