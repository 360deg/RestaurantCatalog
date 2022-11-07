using System.Net;
using System.Text.Json;
using Catalog.Api.Models;
using Common.Exceptions;

namespace Catalog.Api.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    
    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            await HandleException(context, error);
        }
    }

    private static async Task HandleException(HttpContext context, Exception error)
    {
        HttpStatusCode status;
        var message = error.Message;

        status = error switch
        {
            KeyNotFoundException => HttpStatusCode.NotFound,
            BadRequestException => HttpStatusCode.BadRequest,
            NotFoundException => HttpStatusCode.NotFound,
            _ => HttpStatusCode.InternalServerError
        };

        var response = context.Response;
        
        response.ContentType = "application/json";
        response.StatusCode = (int)status;
        
        var result = JsonSerializer.Serialize(new ErrorHandlerResponse { Message = message });

        await response.WriteAsync(result);
    }
}
