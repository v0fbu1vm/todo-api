using Microsoft.OpenApi.Models;
using Todo.Api.Data.Configurations.Dependencies;

namespace Todo.Api.Rest.Configurations.Dependencies
{
    /// <summary>
    /// Registers Swagger UI.
    /// </summary>
    public class SwaggerRegistrant : IServiceRegistrant
    {
        public void Register(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Todo.Api",
                    Description = "A simple Api for managing tasks.",
                    Version = "v1",
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Authorization header. Using bearer scheme."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                          new OpenApiSecurityScheme()
                          {
                                Reference = new OpenApiReference()
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                          },

                         Array.Empty<string>()
                    }
                });
            });
        }
    }
}
