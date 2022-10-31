namespace WebApi;

using System.Net;
using System.Text.Json;
using HttpContent = Microsoft.AspNetCore.Http.HttpContext;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (FluentValidation.ValidationException error)
        {
            await HandleExceptionAsync(context, error);
        }
        catch (Exception error)
        {
            await HandleExceptionAsync(context, error);
        }
    }

    private async Task HandleExceptionAsync(HttpContent context, Exception error)
    {
        var response = context.Response;
        var errorObject = new { message = error?.Message };
        response.ContentType = "application/json";

        switch (error)
        {
            case AppException e:
                // custom application error
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case KeyNotFoundException e:
                // not found error
                response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case FluentValidation.ValidationException e:
                // valdiations errors
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            default:
                // unhandled error
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var result = JsonSerializer.Serialize(errorObject);
        await response.WriteAsync(result);
    }
}
