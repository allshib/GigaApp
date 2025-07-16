using GigaApp.Domain.Models;

namespace GigaApp.Domain.UseCases.GetTopics
{
    public interface IGetTopicsStorage
    {
        Task<(IEnumerable<Topic> topics, int totalCount)> GetTopics(Guid forumId, int skip, int take, CancellationToken cancellationToken);
    }
}
