using GigaApp.Domain.Models;

namespace GigaApp.Domain.UseCases.UpdateForumUseCase;

public interface IUpdateForumStorage
{
    Task Update(Forum forum, CancellationToken cancellationToken);
}