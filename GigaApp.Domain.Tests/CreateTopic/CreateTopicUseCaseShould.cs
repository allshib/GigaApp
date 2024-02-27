using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using GigaApp.Domain.Authorization;
using GigaApp.Domain.Exceptions;
using GigaApp.Domain.Identity;
using GigaApp.Domain.Models;
using GigaApp.Domain.UseCases;
using GigaApp.Domain.UseCases.CreateTopic;
using GigaApp.Domain.UseCases.GetForums;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.Language.Flow;
using System.Security.Principal;
using IIdentity = GigaApp.Domain.Identity.IIdentity;

namespace GigaApp.Domain.Tests
{
    public class CreateTopicUseCaseShould
    {
        private readonly Mock<ICreateTopicStorage> storage;
        private readonly ISetup<ICreateTopicStorage, Task<Models.Topic>> createTopicSetup;
        //private readonly ISetup<ICreateTopicStorage, Task<bool>> forumExistsSetup;
        private Mock<IGetForumsStorage> getForumsStorage;
        private ISetup<IGetForumsStorage, Task<IEnumerable<Forum>>> getForumsSetup;
        private readonly ISetup<IIdentity, Guid> getCurrentIdSetup;
        private readonly Mock<IIntentionManager> intentionManager;
        private readonly ISetup<IIntentionManager, bool> intentionIsAllowedSetup;
        private readonly CreateTopicUseCase sut;

        public CreateTopicUseCaseShould()
        {
            //var dbBuilder = new DbContextOptionsBuilder<ForumDbContext>()
            //    .UseInMemoryDatabase(nameof(CreateTopicUseCaseShould));

            storage = new Mock<ICreateTopicStorage>();
            createTopicSetup = storage.Setup(s =>
                        s.CreateTopic(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));

            //forumExistsSetup = storage.Setup(s =>
            //            s.ForumExists(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

            getForumsStorage = new Mock<IGetForumsStorage>();

            getForumsSetup =getForumsStorage.Setup(s => s.GetForums(It.IsAny<CancellationToken>()));


            var identity = new Mock<IIdentity>();
            var identityProvider = new Mock<IIdentityProvider>();
            identityProvider.Setup(x => x.Current).Returns(identity.Object);
            getCurrentIdSetup = identity.Setup(x => x.UserId);

            intentionManager = new Mock<IIntentionManager>();

            intentionIsAllowedSetup = intentionManager.Setup(x => x.IsAllowed(It.IsAny<TopicIntention>()));

            var validator = new Mock<IValidator<CreateTopicCommand>>();

            validator.Setup(v => v.ValidateAsync(It.IsAny<CreateTopicCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            

            sut = new(intentionManager.Object, storage.Object, getForumsStorage.Object, identityProvider.Object, validator.Object);
        }

        [Fact]
        public async Task ThrowForumNotFoundException_WhenNoForum()
        {
            getForumsSetup.ReturnsAsync(Array.Empty<Forum>());

            intentionIsAllowedSetup.Returns(true);
            var forumId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            (await sut.Invoking(s => s.Execute(new CreateTopicCommand(forumId, "Some Topic"), CancellationToken.None))
                .Should().ThrowAsync<ForumNotFoundException>()).Which.ErrorCode.Should().Be(DomainErrorCode.Gone);
                ;
        }

        [Fact]
        public async Task ReturnNewlyCreatedTopic()
        {
            var forumId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var title = "Some Topic";
            intentionIsAllowedSetup.Returns(true);
            getForumsSetup.ReturnsAsync(new Forum[] {new() { Id = forumId} });
            getCurrentIdSetup.Returns(userId);


            var expected = new Models.Topic { Title = title };
            createTopicSetup.ReturnsAsync(expected);

            var actual = await sut.Execute(new CreateTopicCommand(forumId, title), CancellationToken.None);

            storage.Verify(x =>
                x.CreateTopic(forumId, userId, title, It.IsAny<CancellationToken>()), Times.Once);

        }
        [Fact]
        public async Task ThrowIntentionManagerException_WhenTopicCreationIsNotAlowed()
        {
            var forumId = Guid.NewGuid();
            intentionIsAllowedSetup.Returns(false);
            await sut.Invoking(s => s.Execute(new CreateTopicCommand(forumId, "WharEver"), CancellationToken.None))
                .Should().ThrowAsync<IntetntionManagerExeption>();
            intentionManager.Verify(x => x.IsAllowed(TopicIntention.Create));
        }
    }
}