
using GigaApp.Domain.Identity;

using GigaApp.Domain.Authentication;
using GigaApp.XAF.Blazor.Server.Authentication;


namespace GigaApp.XAF.Blazor.Server.Middlewares;

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
