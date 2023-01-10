using System.Net;

namespace TodoList.Exceptions;

[Serializable]
public class ExistsException : HttpResponseException
{
    public ExistsException()
        : base(HttpStatusCode.Found) { }

    public ExistsException(string message)
        : base(HttpStatusCode.Found, message) { }
}