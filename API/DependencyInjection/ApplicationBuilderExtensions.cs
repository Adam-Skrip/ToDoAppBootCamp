using API.Infrastructure;

namespace API.DependencyInjection;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
        return app;
    }
}