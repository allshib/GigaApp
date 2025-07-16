using GigaApp.Domain.Models;
using MediatR;
using GigaApp.Domain.Exceptions;

namespace GigaApp.Domain.UseCases.GetForumByKey
{
    internal class GetForumByKeyUseCase(IGetForumByKeyStorage storage) : IRequestHandler<GetForumByKeyQuery, Forum>
    {
        public async Task<Forum> Handle(GetForumByKeyQuery request, CancellationToken cancellationToken)
        {
            var forum = await storage.GetForumByKey(request.key, cancellationToken);

            if (forum is null)
                throw new ForumNotFoundException(request.key);

            return forum;
        }
    }
}
