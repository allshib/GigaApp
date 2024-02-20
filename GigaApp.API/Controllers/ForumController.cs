using GigaApp.Domain.UseCases.GetForums;
using GigaApp.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GigaApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForumController : ControllerBase
    {



        public ForumController()
        {
            
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(GigaApp.API.Models.Forum[]))]
        public async Task<IActionResult> GetForums([FromServices] IGetForumsUseCase useCase, CancellationToken cancellationToken)
        {
            var forums = await useCase.Execute(cancellationToken);

            return Ok(forums.Select(x=> new GigaApp.API.Models.Forum {Id = x.Id, Title = x.Title }));
        }
    }
}
