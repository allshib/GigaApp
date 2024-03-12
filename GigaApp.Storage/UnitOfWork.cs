using GigaApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Storage
{
    internal class UnitOfWork : IUnitOfWork
    {
        public async Task<IUnitOfWorkScope> StartScope()
        {
            throw new NotImplementedException();
        }
    }
}
