using GigaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.UseCases.GetTopics
{
    public interface IGetTopicsStorage
    {
        Task<(IEnumerable<Topic> topics, int totalCount)> GetTopics(Guid forumId, int skip, int take, CancellationToken cancellationToken);
    }
}
