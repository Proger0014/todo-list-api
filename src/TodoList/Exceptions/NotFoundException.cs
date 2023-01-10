using System.Net;

namespace TodoList.Exceptions;

[Serializable]
public class NotFoundException : HttpResponseException
{
    public NotFoundException()
        : base(HttpStatusCode.NotFound) { }

    public NotFoundException(string message)
        : base(HttpStatusCode.NotFound, message) { }
}