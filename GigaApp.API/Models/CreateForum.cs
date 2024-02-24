namespace GigaApp.API.Models
{
    /// <summary>
    /// Модель создания форума
    /// </summary>
    public class CreateForum
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public required string  Title { get; init; }
    }
}
