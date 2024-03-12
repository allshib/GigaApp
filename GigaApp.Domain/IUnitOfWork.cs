using GigaApp.Domain.UseCases.CreateTopic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain
{
    public interface IUnitOfWork
    {
        Task<IUnitOfWorkScope> StartScope(CancellationToken cancellationToken);
    }
    public interface IUnitOfWorkScope : IAsyncDisposable
    {
        TStorage GetStorage<TStorage>() where TStorage : IStorage;

        Task Commit(CancellationToken cancellationToken);
    }

    public interface IStorage;
    
}
