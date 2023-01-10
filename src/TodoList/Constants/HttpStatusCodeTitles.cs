using System.Net;

namespace TodoList.Constants;

public static class HttpStatusCodeTitles
{
    public static Dictionary<HttpStatusCode, string> HttpStatusCodeTitlesSet => new()
    {
        { HttpStatusCode.BadRequest, BadRequest },
        { HttpStatusCode.NotFound, NotFound },
        { HttpStatusCode.Found, Found },
        { HttpStatusCode.InternalServerError, InternalServerError }
    };
    public const string BadRequest = "Bad Request";
    public const string NotFound = "Not Found";
    public const string Found = "Found";
    public const string InternalServerError = "Internal Server Error";
}