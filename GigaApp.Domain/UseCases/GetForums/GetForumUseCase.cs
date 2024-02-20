using GigaApp.Domain.Models;
using GigaApp.Storage;
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
        private readonly ForumDbContext context;
        public GetForumUseCase(ForumDbContext context) {
            this.context = context;
        }
        public async Task<IEnumerable<Forum>> Execute(CancellationToken cancellationToken)=>
            await context.Forums.Select(x => new Forum { Id = x.ForumId, Title = x.Title }).ToArrayAsync();
        
    }
}
