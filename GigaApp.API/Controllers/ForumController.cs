using GigaApp.API.Models;
using GigaApp.Domain.UseCases.CreateTopic;
using GigaApp.Domain.UseCases.GetForums;
using GigaApp.Domain.UseCases.GetTopics;
using Microsoft.AspNetCore.Mvc;
using Topic = GigaApp.API.Models.Topic;

namespace GigaApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForumController : ControllerBase
    {



        public ForumController()
        {

        }

        [HttpGet(Name = nameof(GetForums))]
        [ProducesResponseType(200, Type = typeof(GigaApp.API.Models.Forum[]))]
        public async Task<IActionResult> GetForums([FromServices] IGetForumsUseCase useCase, CancellationToken cancellationToken)
        {
            var forums = await useCase.Execute(cancellationToken);

            return Ok(forums.Select(x => new GigaApp.API.Models.Forum { Id = x.Id, Title = x.Title }));
        }

        [HttpGet("{forumId:guid}/topics")]
        [ProducesResponseType(400)]
        [ProducesResponseType(410)]
        [ProducesResponseType(200)]

        public async Task<IActionResult> GetTopics(
            [FromRoute]  Guid forumId, 
            [FromQuery] int skip, 
            [FromQuery] int take, 
            [FromServices] IGetTopicsUseCase useCase, 
            CancellationToken cancellationToken)
        {
            var (resources, total) = await useCase.Execute(new GetTopicsQuery(forumId, skip, take), cancellationToken);

            return Ok(new { resources = resources.Select(x=> new Topic
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt,
                    Title = x.Title
                }), 
                total});

        }


        [HttpPost("{forumId:guid}/topics")]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(410)]
        [ProducesResponseType(201, Type = typeof(Topic))]
        public async Task<IActionResult> CreateTopic(Guid forumId, 
            [FromBody] CreateTopic createTopic,
            [FromServices] ICreateTopicUseCase useCase,
            CancellationToken cancellationToken)
        {

            var command = new CreateTopicCommand(forumId, createTopic.Title);
            var topic = await useCase.Execute(command, cancellationToken);

            return CreatedAtRoute(nameof(GetForums), new Topic
            {
                Id = topic.Id,
                Title = topic.Title,
                CreatedAt = topic.CreatedAt,
            });

        }
    }
}
