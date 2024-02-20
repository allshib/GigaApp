using GigaApp.Domain.UseCases.CreateTopic;

namespace GigaApp.Domain.Tests
{
    public class CreateTopicUseCaseShould
    {
        private readonly CreateTopicUseCase sut;
        public CreateTopicUseCaseShould()
        {
            sut = new CreateTopicUseCase();
        }

        [Fact]
        public void ThrowForumNotFoundException_WhenNoForum()
        {
            var forumId = Guid.NewGuid();
            var authorId = Guid.NewGuid();

            sut.Execute(forumId, "Some Topic", authorId, null);
        }
    }
}