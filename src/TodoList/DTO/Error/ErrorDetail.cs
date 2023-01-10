using System.Net;
using TodoList.Extensions;

namespace TodoList.DTO.Error;

public class ErrorDetail
{
    public string Title { get; private set; }
    public string Message { get; set; }
    public int Status { get; private set; }

    public ErrorDetail(HttpStatusCode status)
    {
        Title = status.GetStatusTitle();
        Message = string.Empty;
        Status = (int)status;
    }
}