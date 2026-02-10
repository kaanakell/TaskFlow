using System.Net;
using System.Text.Json;

namespace TaskFlow.Api.Middleware;

public class ExceptionHandlingMiddleWare
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                error = "An unexpected error occurred.",
                detail = ex.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}