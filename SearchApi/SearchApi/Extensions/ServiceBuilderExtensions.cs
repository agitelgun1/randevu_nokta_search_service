using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using RandevuNokta.Search.Api.Helper;
using RandevuNokta.Search.Api.ServiceInterfaces;
using RandevuNokta.Search.Api.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace RandevuNokta.Search.Api.Extensions
{
    public static class ServiceBuilderExtensions
    {
        public static void ConfigureSingletonCollections(this IServiceCollection services)
        {
            services.AddSingleton<ISolrSearch, SolrSearch>();
            services.AddSingleton<IClinicSearch,ClinicSearch>();
            services.AddSingleton<IDoctorSearch,DoctorSearch>();
            services.AddSingleton<ICoreSearch,CoreSearch>();
            services.AddSingleton<IConnectionHelper, ConnectionHelper>();
            services.AddSingleton<IErrorService, ErrorService>();
        }
        
        public static void ConfigureSwaggerGenCollection(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // c.OperationFilter<AddRequiredHeaderParameter>();
                c.SwaggerDoc("v1", new Info { Title = "Randevu Nokta Search Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    { "Bearer", Enumerable.Empty<string>() },
                });
            });
        }
        
        public static void ConfigureCorsCollection(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });
        }
    }
}