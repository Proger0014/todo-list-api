using System.Text.RegularExpressions;
using System.Net;
using TodoList.Constants;
using TodoList.Utils;

namespace TodoList.Extensions;

public static class CommonExtensions
{
    public static string GetStatusTitle(this HttpStatusCode statusCode)
    {
        return HttpStatusCodeTitles.HttpStatusCodeTitlesSet[statusCode];
    }
}
