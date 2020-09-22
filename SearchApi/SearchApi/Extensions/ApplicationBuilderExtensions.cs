using Microsoft.AspNetCore.Builder;
using RandevuNokta.Search.Api.Middleware;

namespace RandevuNokta.Search.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseErrorMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ErrorMiddleware>();
        }

        public static void UseSwaggerUIBuilder(this IApplicationBuilder builder)
        {
            builder.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Search Api"); });
        }

        public static void UseCorsBuilder(this IApplicationBuilder builder)
        {
            builder.UseCors("AllowAll");
        }
    }
}