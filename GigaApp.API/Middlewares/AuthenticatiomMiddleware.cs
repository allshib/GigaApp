using GigaApp.API.Authentication;
using GigaApp.Domain.Authentication;
using GigaApp.Domain.Identity;

namespace GigaApp.API.Middlewares
{
    public class AuthenticatiomMiddleware
    {
        private readonly RequestDelegate next;

        public AuthenticatiomMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(
            HttpContext httpContext,
            IAuthTokenStorage tokenStorage,
            IAuthenticationService authenticationService, 
            IIdentityProvider identitySetter, 
            CancellationToken cancellationToken
            )
        {
            var identity = tokenStorage.TryExtract(httpContext, out var authToken)
                ? await authenticationService.Authenticate(authToken, cancellationToken)
                : User.Guest;

            identitySetter.Current = identity;
            await next(httpContext);
        }

    }
}
