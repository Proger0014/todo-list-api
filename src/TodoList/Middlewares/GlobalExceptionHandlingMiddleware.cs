using TodoList.DTO.Error;
using TodoList.Exceptions;
using System.Net;

namespace TodoList.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (HttpResponseException e)
        {
            context.Response.StatusCode = e.StatusCode;

            if (string.IsNullOrEmpty(e.Message))
            {
                await context.Response
                    .WriteAsJsonAsync(new ErrorDetailMini((HttpStatusCode)e.StatusCode));

                return;
            }

            await context.Response
                .WriteAsJsonAsync(new ErrorDetail((HttpStatusCode)e.StatusCode)
                {
                    Message = e.Message
                });
        }
    }
}