using System.Net;

namespace TodoList.Exceptions;

[Serializable]
public class TokenExpiredException : HttpResponseException
{
    public TokenExpiredException()
        : base(HttpStatusCode.BadRequest) { }

    public TokenExpiredException(string message)
        : base(HttpStatusCode.BadRequest, message) { }
}