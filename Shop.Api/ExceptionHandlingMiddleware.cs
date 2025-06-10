using Shop.Core.Exceptions;
using System.Data;
using System.Text.Json;

namespace Shop.Api;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate requestDelegate;
    private readonly ILogger<ExceptionHandlingMiddleware> logger;

    public ExceptionHandlingMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlingMiddleware> logger)
    {
        ArgumentNullException.ThrowIfNull(requestDelegate, nameof(requestDelegate));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        this.requestDelegate = requestDelegate;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this.requestDelegate(context);
        }
        catch (ArgumentNullException ex)
        {
            this.logger.LogError(ex, "ArgumentNullException");
            await WriteErrorResponseAsync(context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (ArgumentException ex)
        {
            this.logger.LogError(ex, "ArgumentException");
            await WriteErrorResponseAsync(context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (NotFoundException ex)
        {
            this.logger.LogError(ex, "NotFoundException");
            await WriteErrorResponseAsync(context, StatusCodes.Status404NotFound, ex.Message);
        }
        catch (DuplicateNameException ex)
        {
            this.logger.LogError(ex, "DuplicateNameException");
            await WriteErrorResponseAsync(context, StatusCodes.Status409Conflict, ex.Message);
        }
        catch (AggregateException ex) when (ex.InnerException is InvalidOperationException)
        {
            this.logger.LogError(ex.InnerException, "InvalidOperationException (wrapped)");
            await WriteErrorResponseAsync(context, StatusCodes.Status409Conflict, ex.InnerException.Message);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Unhandled exception");
            await WriteErrorResponseAsync(context, StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    private async Task WriteErrorResponseAsync(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        var payload = new
        {
            status = statusCode,
            message
        };
        var json = JsonSerializer.Serialize(payload);
        await context.Response.WriteAsync(json);
    }
}
