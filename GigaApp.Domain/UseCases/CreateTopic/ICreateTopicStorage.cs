﻿using GigaApp.Domain.Models;

namespace GigaApp.Domain.UseCases.CreateTopic
{
    public interface ICreateTopicStorage : IStorage
    {
        //Task<bool> ForumExists(Guid forumId, CancellationToken cancellationToken);
        Task<Topic> CreateTopic(Guid forumId,Guid userId, string title, CancellationToken cancellationToken);
    }
}
