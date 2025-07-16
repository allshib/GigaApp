using GigaApp.Domain.Models;

namespace GigaApp.Domain.UseCases.CreateForum
{
    public interface ICreateForumStorage : IStorage
    {
        Task<Forum?> Create(string title, CancellationToken cancellationToken);
    }

    
}
