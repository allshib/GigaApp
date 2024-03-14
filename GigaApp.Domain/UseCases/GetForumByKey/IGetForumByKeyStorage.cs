using GigaApp.Domain.Models;

namespace GigaApp.Domain.UseCases.GetForumByKey;

public interface IGetForumByKeyStorage
{
    Task<Forum?> GetForumByKey(Guid id, CancellationToken cancellationToken);
}