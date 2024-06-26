﻿using GigaApp.API.Authentication;
using GigaApp.API.Models;
using GigaApp.Domain.UseCases.SignIn;
using GigaApp.Domain.UseCases.SignOn;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog.Core;
using Serilog.Events;

namespace GigaApp.API.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator useCase;


        public AccountController(IMediator useCase)
        {
            this.useCase = useCase;
        }


        [HttpPost]
        public async Task<IActionResult> SignOn(
            [FromBody] SignOn request,
            CancellationToken cancellationToken
            )
        {
            var identity = await useCase
                .Send(new SignOnCommand(request.Login, request.Password), cancellationToken);

            return Ok(identity);
        }


        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(
            [FromBody] SignIn request,
            [FromServices] IAuthTokenStorage tokenStorage,
            CancellationToken cancellationToken
            )
        {
            var (identity, token) = await useCase
                .Send(new SignInCommand(request.Login, request.Password), cancellationToken);

            tokenStorage.Store(HttpContext, token);

            return Ok(identity);
        }


        [HttpGet("switch-log-level")]
        public Task<IActionResult> SwitchLogLevel(
            [FromServices] LoggingLevelSwitch loggingLevelSwitch,
            CancellationToken cancellationToken)
        {
            loggingLevelSwitch.MinimumLevel = loggingLevelSwitch.MinimumLevel switch {
                LogEventLevel.Information => LogEventLevel.Error,
                _ => LogEventLevel.Information
            };

            return Task.FromResult((IActionResult)Ok());
        }
    }
}
