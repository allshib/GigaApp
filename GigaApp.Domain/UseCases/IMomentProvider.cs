namespace GigaApp.Domain.UseCases
{
    public interface IMomentProvider
    {
        DateTimeOffset Now { get; }
    }

    public class MomentProvider : IMomentProvider
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}
