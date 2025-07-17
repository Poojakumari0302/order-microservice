using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace Api.Configuration.Middleware;

public sealed class ApiLoggingMiddleware(RequestDelegate next, ILogger<ApiLoggingMiddleware> logger)
{
    private const string MessageTemplate =
       "An error occurred while executing HTTP {RequestMethod} {RequestPath}";

    private const string CustomDataPrefix = "CustomData";

    private readonly RequestDelegate _next = next;
    private readonly ILogger<ApiLoggingMiddleware> _logger = logger;

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        // Never caught, because `LogException()` returns false.
        catch (Exception ex) when (LogException(httpContext, ex)) { }
    }

    private bool LogException(HttpContext httpContext, Exception ex)
    {
        var request = httpContext.Request;


        using (LogContext.PushProperty(CustomDataPrefix, new
        {
            RequestQueryString = request.QueryString.Value,
            User = httpContext.User.Identity.Name ?? "Anonymous"
        }, true))
        {
            _logger.LogError(ex, MessageTemplate, httpContext.Request.Method, httpContext.Request.Path);
        }

        return false;
    }
}
