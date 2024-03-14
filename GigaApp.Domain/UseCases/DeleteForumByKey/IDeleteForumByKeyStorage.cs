namespace GigaApp.Domain.UseCases.DeleteForumByKey;

public interface IDeleteForumByKeyStorage
{
    Task DeleteByKey(Guid key, CancellationToken cancellationToken);
}