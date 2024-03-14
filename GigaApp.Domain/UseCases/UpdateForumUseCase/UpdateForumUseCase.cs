using GigaApp.Domain.UseCases.GetForums;
using GigaApp.Domain.UseCases.SignOn;
using MediatR;

namespace GigaApp.Domain.UseCases.UpdateForumUseCase;

internal class UpdateForumUseCase(IUpdateForumStorage storage, IGetForumsStorage getForumsStorage) : IRequestHandler<UpdateForumCommand>
{
    public async Task Handle(UpdateForumCommand request, CancellationToken cancellationToken)
    {
        await getForumsStorage.ThrowIfForumWasNotFound(request.Forum.Id, cancellationToken);

        await storage.Update(request.Forum, cancellationToken);
    }
}