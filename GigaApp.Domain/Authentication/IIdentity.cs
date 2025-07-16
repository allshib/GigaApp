namespace GigaApp.Domain.Identity
{
    /// <summary>
    /// Отвечает за аутентификацию пользователя
    /// </summary>
    public interface IIdentity
    {
        Guid UserId { get; }
        Guid SessionId { get; }
    }


    /// <summary>
    /// Проверяет, авторизован ли пользователь
    /// </summary>
    public static class IdentityEx
    {
        public static bool IsAuthenticated(this IIdentity identity) => identity.UserId != Guid.Empty;
    }
}
