using GigaApp.Domain.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigaApp.Domain.Monitoring;
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
