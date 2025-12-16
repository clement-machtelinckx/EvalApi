using EvalApi.Src.Core.Exceptions;

namespace EvalApi.Src.Core.Middlewares;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            await WritePlainTextAsync(context, StatusCodes.Status404NotFound, ex.Message);
        }
        catch (BadRequestException ex)
        {
            await WritePlainTextAsync(context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch
        {
            await WritePlainTextAsync(context, StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    private static async Task WritePlainTextAsync(HttpContext context, int statusCode, string message)
    {
        if (context.Response.HasStarted)
            return;

        context.Response.Clear();
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "text/plain; charset=utf-8";
        await context.Response.WriteAsync(message);
    }
}
