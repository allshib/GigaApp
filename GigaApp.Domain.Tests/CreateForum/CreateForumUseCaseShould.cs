using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using GigaApp.Domain.Authorization;
using GigaApp.Domain.Models;
using GigaApp.Domain.UseCases.CreateForum;
using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Tests.CreateForum
{
    public class CreateForumUseCaseShould
    {
        private readonly ISetup<ICreateForumStorage, Task<Forum>> createForumStorageSetup;
        private readonly CreateForumUseCase sut;
        private readonly ISetup<IIntentionManager, bool> intentionManagerSetup;
        private readonly Mock<ICreateForumStorage> storage;

        public CreateForumUseCaseShould()
        {
            var validator = new Mock<IValidator<CreateForumCommand>>();

            validator
                .Setup(v => v.ValidateAsync(It.IsAny<CreateForumCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var intentionManager = new Mock<IIntentionManager>();

            intentionManagerSetup = intentionManager.Setup(x => x.IsAllowed(It.IsAny<ForumIntention>()));

            storage = new Mock<ICreateForumStorage>();
            createForumStorageSetup = storage.Setup(x => x.Create(It.IsAny<string>(), It.IsAny<CancellationToken>()));

            sut = new CreateForumUseCase(validator.Object, intentionManager.Object, storage.Object);
        }

        [Fact]
        public async Task ReturnCreatedForum()
        {
            var forum = new Forum
            {
                Id = Guid.Parse("7532eb2a-e4df-4c4d-94be-df3072374e21"),
                Title = "Test"
            };
            createForumStorageSetup.ReturnsAsync(forum);
            intentionManagerSetup.Returns(true);

            var actual = await sut.Handle(new CreateForumCommand("Test"), CancellationToken.None);

            actual.Should().Be(forum);
            storage.Verify(x => x.Create("Test", CancellationToken.None), Times.Once);
            storage.VerifyNoOtherCalls();
        }


        //[Fact]
        //public async Task ReturnCreatedForum()
        //{
        //    var forum = new Forum
        //    {
        //        Id = Guid.Parse("7532eb2a-e4df-4c4d-94be-df3072374e21"),
        //        Title = "Test"
        //    };
        //    createForumStorageSetup.ReturnsAsync(forum);
        //    intentionManagerSetup.Returns(true);

        //    var actual = await sut.Execute(new CreateForumCommand("Test"), CancellationToken.None);

        //    actual.Should().Be(forum);
        //}

    }
}
