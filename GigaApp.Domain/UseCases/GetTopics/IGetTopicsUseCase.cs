using GigaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.UseCases.GetTopics
{
    public interface IGetTopicsUseCase
    {
        Task<(IEnumerable<Topic> resources, int totalCount)> Execute(
            GetTopicsQuery query, CancellationToken cancellationToken);
    }
}
