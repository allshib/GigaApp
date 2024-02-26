using GigaApp.API.Authentication;
using GigaApp.Domain.Identity;

using GigaApp.Domain.Authentication;

namespace GigaApp.API.Middlewares;

public class AuthenticationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext httpContext,
        IAuthTokenStorage tokenStorage,
        IAuthenticationService authenticationService,
        IIdentityProvider identityProvider)
    {
        var identity = tokenStorage.TryExtract(httpContext, out var authToken)
            ? await authenticationService.Authenticate(authToken, httpContext.RequestAborted)
            : User.Guest;
        identityProvider.Current = identity;

        await next(httpContext);
    }
}