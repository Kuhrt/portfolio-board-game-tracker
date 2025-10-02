using BoardGameTracker.Common.Exceptions;
using System.Net.Mime;
using System.Net;
using System.Text.Json;
using BoardGameTracker.Core.Models.Api;

namespace BoardGameTracker.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var response = context.Response;
            response.ContentType = MediaTypeNames.Application.Json;
            bool logException = false;
            var exceptionName = "Exception";

            switch (exception)
            {
                case BadRequestException _:
                    {
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                    break;
                case NotFoundException _:
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                    }
                    break;
                case FileNotFoundException _:
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                    }
                    break;
                case ForbiddenException _:
                    {
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                    }
                    break;
                case ConflictException _:
                    {
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                    }
                    break;
                case NotAuthorizedException _:
                    {
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    }
                    break;

                default:
                    // unhandled error
                    logException = true;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            if (logException)
            {
                logger.LogError($"[{exceptionName}]: {exception.Message}"
                        + (exception.InnerException != null ? $"\nInner Exception: {exception.InnerException.Message}" : string.Empty));
            }

            var result = JsonSerializer.Serialize(new ApiResponse<object> { Error = exception?.Message });
            await response.WriteAsync(result);
        }
    }
}