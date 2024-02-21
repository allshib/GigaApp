using GigaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.UseCases.GetForums
{
    public interface IGetForumsStorage
    {
        Task<IEnumerable<Forum>> GetForums(CancellationToken cancellationToken);
    }
}
