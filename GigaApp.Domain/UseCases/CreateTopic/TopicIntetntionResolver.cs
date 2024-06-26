﻿using GigaApp.Domain.Authorization;
using GigaApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.UseCases.CreateTopic
{
    /// <summary>
    /// Разрешения, связанные с Topic
    /// </summary>
    internal class TopicIntetntionResolver : IIntentionResolver<TopicIntention>
    {
        public bool IsAllowed(IIdentity subject, TopicIntention intention) => intention switch
        {
            TopicIntention.Create => subject.IsAuthenticated(),
            _ => false
        };
        
    }
}
