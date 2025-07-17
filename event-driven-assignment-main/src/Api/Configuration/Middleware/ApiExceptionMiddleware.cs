using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Api.Configuration.Models.v1;
using Domain.Shared.Exceptions;

namespace Api.Configuration.Middleware;

internal sealed class ApiExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        int statusCode;
        string category, message;

        if (exception is BaseException domainException)
        {
            statusCode = (int)GetDomainErrorStatusCode(domainException);
            category = domainException.Category;
            message = domainException.Message;
        }
        else
        {
            statusCode = (int)HttpStatusCode.InternalServerError;
            category = "System Error";
            message = "An internal system error occurred";
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var error = new ErrorResponse
        {
            Error = new ErrorModel
            {
                Category = category,
                Message = message
            }
        };
        
        var jsonErrorResult = JsonSerializer.Serialize(error, _jsonOptions);

        await context.Response.WriteAsync(jsonErrorResult);
    }

    private static HttpStatusCode GetDomainErrorStatusCode(BaseException exception)
    {
        return exception.Category switch
        {
            "Duplicate Error" => HttpStatusCode.Conflict,
            "Not Found Error" => HttpStatusCode.NotFound,
            _ => HttpStatusCode.BadRequest,
        };
    }
}