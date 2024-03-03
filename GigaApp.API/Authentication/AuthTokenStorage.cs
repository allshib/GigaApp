namespace GigaApp.API.Authentication
{
    internal class AuthTokenStorage : IAuthTokenStorage
    {
        private const string HeaderKey = "GigaApp-Auth-Token";
        public void Store(HttpContext httpContext, string token)
        {
            httpContext.Response.Cookies.Append(HeaderKey, token, new CookieOptions
            {
                //HttpOnly = true,
                //SameSite = SameSiteMode.Strict,
                //Secure = true
            });
        }

        public bool TryExtract(HttpContext httpContext, out string token)
        {
            if (httpContext.Request.Cookies.TryGetValue(HeaderKey, out var value) &&
                !string.IsNullOrWhiteSpace(value))
            {
                token = value;
                return true;
            }

            token = string.Empty;
            return false;
        }
    }
}
