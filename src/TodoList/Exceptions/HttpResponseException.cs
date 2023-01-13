using System.Net;

namespace TodoList.Exceptions;

public abstract class HttpResponseException : Exception
{
    public int StatusCode { get; set; }

    public HttpResponseException()
        : this("") { }

    public HttpResponseException(string message)
        : base(message)
    {
        StatusCode = (int)HttpStatusCode.InternalServerError;
    }

    public HttpResponseException(HttpStatusCode statusCode)
        : this(statusCode, "") { }

    public HttpResponseException(HttpStatusCode statusCode, string message)
        : base(message)
    {
        StatusCode = (int)statusCode;
    }
}