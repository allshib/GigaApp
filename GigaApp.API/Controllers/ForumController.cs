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
        public async Task<IActionResult> GetForums([FromServices] ForumDbContext dbContext, CancellationToken cancellationToken)
        {
            return Ok(await dbContext.Forums.Select(x => x.Title).ToArrayAsync(cancellationToken));
        }
    }
}
