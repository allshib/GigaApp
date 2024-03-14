namespace GigaApp.XAF.Blazor.Server.Authentication
{
    public interface IAuthTokenStorage
    {
        bool TryExtract(HttpContext httpContext, out string token);
        void Store(HttpContext httpContext, string token);
    }
}
