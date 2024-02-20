using GigaApp.Domain.Models;
using GigaApp.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topic = GigaApp.Domain.Models.Topic;

namespace GigaApp.Domain.UseCases.CreateTopic
{
    public class CreateTopicUseCase : ICreateTopicUseCase
    {

        private readonly ForumDbContext context;
        public CreateTopicUseCase(ForumDbContext context)
        {
            this.context = context;
        }

        public Task<Topic> Execute(Guid forumId, string title, Guid authorId, CancellationToken cancellationToken)
        {
            var 
        }
    }
}
