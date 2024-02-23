using GigaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.UseCases.CreateTopic
{
    public interface ICreateTopicUseCase
    {
        Task<Topic> Execute(CreateTopicCommand command, CancellationToken cancellationToken);
    }
}
