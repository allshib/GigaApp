namespace GigaApp.Domain.UseCases.CreateTopic
{
    public record CreateTopicCommand(Guid ForumId, string Title);
}
