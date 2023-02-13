namespace TodoList.Services.DateTimeProvider;

public class DefaultDateTimeProvider : IDateTimeProvider
{
    public DateTime DateTimeNow { get; init; } = DateTime.Now;
}