using System.Net;
using TodoList.Extensions;

namespace TodoList.DTO.Error;

public class ErrorDetailMini
{
    public string Title { get; private set; }
    public int Status { get; private set; }

    public ErrorDetailMini(HttpStatusCode status)
    {
        Title = status.GetStatusTitle();
        Status = (int)status;
    }
}