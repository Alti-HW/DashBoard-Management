using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Dashboard_Management.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddSwagger(this IServiceCollection service, ConfigurationManager configuration)
        {
            service.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(id => id.FullName!.Replace('+', '-'));

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token only",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.OAuth2,
                    Scheme = configuration["keycloak:Scheme"]!,
                    BearerFormat = configuration["keycloak:BearerFormat"]!,
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    },
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(configuration["keycloak:AuthorizationUrl"]!),
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "OpenID Connect scope" },
                                { "profile", "User profile scope" },
                                { "email", "User email scope" },
                                { "roles", "roles" }
                            },
                        }
                    }
                };

                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { "openid", "profile", "email", "roles" }}
                });
            });
        }
    }
}
