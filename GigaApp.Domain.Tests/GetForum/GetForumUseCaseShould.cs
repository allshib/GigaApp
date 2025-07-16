using FluentAssertions;
using GigaApp.Domain.Models;
using GigaApp.Domain.UseCases.GetForums;
using Moq;
using Moq.Language.Flow;

namespace GigaApp.Domain.Tests.GetForum
{
    public class GetForumUseCaseShould
    {
        private readonly Mock<IGetForumsStorage> storage;

        public ISetup<IGetForumsStorage, Task<IEnumerable<Forum>>> storageSetup { get; }

        private readonly GetForumUseCase sut;

        public GetForumUseCaseShould()
        {
            storage = new Mock<IGetForumsStorage>();
            storageSetup = storage.Setup(x => x.GetForums(It.IsAny<CancellationToken>()));


            sut = new GetForumUseCase(storage.Object);
        }


        [Fact]
        public async Task ReturnForums_FromStorage()
        {
            var forums = new Forum[] {
                    new() { Id = Guid.Parse("9601a112-01bb-4fa6-9957-70595cad0d5c"), Title = "Test" }
                };

            storageSetup.ReturnsAsync(forums);

            var actual = await sut.Handle( new GetForumsQuery(), CancellationToken.None);

            actual.Should().BeSameAs(forums);
            storage.Verify(x => x.GetForums(CancellationToken.None), Times.Once);
            storage.VerifyNoOtherCalls();
        }
    }
}
