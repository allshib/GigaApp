
using GigaApp.Domain.UseCases.SignIn;
using GigaApp.Domain.UseCases.SignOn;
using GigaApp.XAF.Blazor.Server.Authentication;
using GigaApp.XAF.Blazor.Server.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace GigaApp.XAF.Blazor.Server.Controllers
{
    [ApiController]
    [Route("accountblazor")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator useCase;


        public AccountController(IMediator useCase)
        {
            this.useCase = useCase;
        }



        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(
            [FromBody] SignIn request,
            [FromServices] IAuthTokenStorage tokenStorage,
            CancellationToken cancellationToken
            )
        {
            tokenStorage.Store(HttpContext, request.Token);

            return Ok();
        }

    }
}
