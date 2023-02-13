namespace TodoList.Services.DateTimeProvider;

public interface IDateTimeProvider
{
    public DateTime DateTimeNow { get; init; }
}