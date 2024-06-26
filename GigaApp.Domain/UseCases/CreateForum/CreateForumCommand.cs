﻿using GigaApp.Domain.Models;
using GigaApp.Domain.Monitoring;
using MediatR;

namespace GigaApp.Domain.UseCases.CreateForum
{
    /// <summary>
    /// Команда создания форума
    /// </summary>
    /// <param name="Title"></param>
    public record CreateForumCommand(string Title) : IRequest<Forum>, IMonitoredRequest
    {
        private const string CounterName = "forum.created";

        public void MonitorFailure(DomainMetrics metrics)
        {
            metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(false));
        }

        public void MonitorSuccess(DomainMetrics metrics)
        {
            metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(true));
        }
    }
}