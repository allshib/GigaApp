using GigaApp.API.Models;
using GigaApp.Domain.Exceptions;
using GigaApp.Domain.Models;
using GigaApp.Domain.UseCases.CreateTopic;
using GigaApp.Domain.UseCases.GetForums;
using GigaApp.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        [HttpPost("{forumId:guid}/topics")]
        [ProducesResponseType(403)]
        [ProducesResponseType(410)]
        [ProducesResponseType(201, Type = typeof(Topic))]
        public async Task<IActionResult> CreateTopic(Guid forumId, 
            [FromBody] CreateTopic createTopic,
            [FromServices] ICreateTopicUseCase useCase,
            CancellationToken cancellationToken)
        {
            try
            {
                var topic = await useCase.Execute(forumId, createTopic.Title, cancellationToken);

                return CreatedAtRoute(nameof(GetForums), new Topic
                {
                    Id = topic.Id,
                    Title = topic.Title,
                    CreatedAt = DateTimeOffset.Now,
                });
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    IntetntionManagerExeption => Forbid(),
                    ForumNotFoundException => StatusCode(StatusCodes.Status410Gone),
                    _ => StatusCode(StatusCodes.Status500InternalServerError)
                };
            }
        }
    }
}
