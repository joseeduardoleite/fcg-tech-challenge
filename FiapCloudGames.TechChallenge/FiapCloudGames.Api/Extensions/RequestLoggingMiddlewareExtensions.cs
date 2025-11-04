using FiapCloudGames.Api.Middleware;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        => builder.UseMiddleware<RequestLoggingMiddleware>();
}