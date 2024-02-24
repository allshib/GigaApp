using AutoMapper;
using GigaApp.API.Models;
using GigaApp.Domain.UseCases.CreateForum;
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


        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(201, Type = typeof(Forum))]
        public async Task<IActionResult> CreateForum(Guid forumId,
            [FromBody] CreateForum createForum,
            [FromServices] ICreateForumUseCase useCase,
            [FromServices] IMapper mapper,
            CancellationToken cancellationToken)
        {

            var command = new CreateForumCommand(createForum.Title);
            var forum = await useCase.Execute(command, cancellationToken);

            return CreatedAtRoute(nameof(GetForums), mapper.Map<Forum>(forum));

        }


        /// <summary>
        /// Получает список форумов
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetForums))]
        [ProducesResponseType(200, Type = typeof(Forum[]))]
        public async Task<IActionResult> GetForums(
            [FromServices] IGetForumsUseCase useCase,
            [FromServices] IMapper mapper,
            CancellationToken cancellationToken)
        {
            var forums = await useCase.Execute(cancellationToken);

            return Ok(forums.Select(mapper.Map<Forum>));
        }




        /// <summary>
        /// Получает список топиков
        /// </summary>
        /// <param name="forumId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="useCase"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{forumId:guid}/topics")]
        [ProducesResponseType(400)]
        [ProducesResponseType(410)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetTopics(
            [FromRoute]  Guid forumId, 
            [FromQuery] int skip, 
            [FromQuery] int take, 
            [FromServices] IGetTopicsUseCase useCase,
            [FromServices] IMapper mapper,
            CancellationToken cancellationToken)
        {
            var (resources, total) = await useCase.Execute(new GetTopicsQuery(forumId, skip, take), cancellationToken);

            return Ok(new { resources = resources.Select(mapper.Map<Topic>), total});

        }

        /// <summary>
        /// Создает топик
        /// </summary>
        /// <param name="forumId"></param>
        /// <param name="createTopic"></param>
        /// <param name="useCase"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("{forumId:guid}/topics")]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(410)]
        [ProducesResponseType(201, Type = typeof(Topic))]
        public async Task<IActionResult> CreateTopic(Guid forumId, 
            [FromBody] CreateTopic createTopic,
            [FromServices] ICreateTopicUseCase useCase,
            [FromServices] IMapper mapper,
            CancellationToken cancellationToken)
        {

            var command = new CreateTopicCommand(forumId, createTopic.Title);
            var topic = await useCase.Execute(command, cancellationToken);

            return CreatedAtRoute(nameof(GetForums), mapper.Map<Topic>(topic));

        }
    }
}
