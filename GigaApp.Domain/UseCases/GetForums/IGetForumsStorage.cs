using GigaApp.Domain.Models;

namespace GigaApp.Domain.UseCases.GetForums
{
    public interface IGetForumsStorage
    {
        Task<IEnumerable<Forum>> GetForums(CancellationToken cancellationToken);
        IEnumerable<Forum> GetForumsNotAsync();
    }
}
