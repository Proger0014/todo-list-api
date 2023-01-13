using System.Net;

namespace TodoList.Exceptions;

public class AccessDeniedException : HttpResponseException
{
    public AccessDeniedException()
        : base(HttpStatusCode.Forbidden) { }

    public AccessDeniedException(string message)
        : base(HttpStatusCode.Forbidden, message) { }
}