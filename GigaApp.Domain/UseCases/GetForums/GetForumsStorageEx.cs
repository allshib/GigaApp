using GigaApp.Domain.Exceptions;

namespace GigaApp.Domain.UseCases.GetForums
{
    internal static class GetForumsStorageEx
    {

        public static async Task<bool> ForumExists(this IGetForumsStorage storage, Guid forumId, CancellationToken cancellationToken)
        {
            var forums = await storage.GetForums(cancellationToken);

            return forums.Any(f=>f.Id == forumId);
        }

        public static async Task ThrowIfForumWasNotFound(this IGetForumsStorage storage, Guid forumId, CancellationToken cancellationToken)
        {
            if(!await ForumExists(storage, forumId, cancellationToken))
            {
                throw new ForumNotFoundException(forumId);
            }
        }
    }
}
