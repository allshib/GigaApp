using MediatR;
using Forum = GigaApp.Domain.Models.Forum;


namespace GigaApp.Domain.UseCases.GetForums
{
    internal class GetForumUseCase(IGetForumsStorage getForumsStorage) : IRequestHandler<GetForumsQuery, IEnumerable<Forum>>
    {
        public async Task<IEnumerable<Forum>> Handle(GetForumsQuery request, CancellationToken cancellationToken)
        {
            var forums = await getForumsStorage.GetForums(cancellationToken);

            return forums;
        }
    }
}
