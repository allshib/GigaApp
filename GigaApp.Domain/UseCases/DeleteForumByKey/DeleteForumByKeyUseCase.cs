using GigaApp.Domain.UseCases.GetForums;
using MediatR;

namespace GigaApp.Domain.UseCases.DeleteForumByKey
{
    internal class DeleteForumByKeyUseCase(IDeleteForumByKeyStorage storage, IGetForumsStorage getForumsStorage) : IRequestHandler<DeleteForumByKeyCommand>
    {
        public async Task Handle(DeleteForumByKeyCommand request, CancellationToken cancellationToken)
        {

            await getForumsStorage.ThrowIfForumWasNotFound(request.ForumId, cancellationToken);

            await storage.DeleteByKey(request.ForumId, cancellationToken);
        }
    }
}
