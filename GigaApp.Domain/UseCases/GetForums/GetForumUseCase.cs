using GigaApp.Domain.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forum = GigaApp.Domain.Models.Forum;


namespace GigaApp.Domain.UseCases.GetForums
{
    public class GetForumUseCase : IGetForumsUseCase
    {
        private readonly IGetForumsStorage getForumsStorage;

        public GetForumUseCase(IGetForumsStorage getForumsStorage)
        {
            this.getForumsStorage = getForumsStorage;
        }
        public async Task<IEnumerable<Forum>> Execute(CancellationToken cancellationToken) =>
            await getForumsStorage.GetForums(cancellationToken);

    }
}
