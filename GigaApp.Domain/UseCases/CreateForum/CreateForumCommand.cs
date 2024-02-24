namespace GigaApp.Domain.UseCases.CreateForum
{
    /// <summary>
    /// Команда создания форума
    /// </summary>
    /// <param name="Title"></param>
    public record CreateForumCommand(string Title);
    
}