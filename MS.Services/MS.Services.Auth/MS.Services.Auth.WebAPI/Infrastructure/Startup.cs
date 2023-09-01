using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using MS.Libs.Core.Domain.Constants;
using MS.Libs.Core.Domain.Infra.AppSettings;
using MS.Libs.Core.Domain.Infra.Claims;
using MS.Libs.Core.Domain.Models.Dto;
using MS.Libs.Core.Domain.Models.Error;
using MS.Libs.Infra.Utils.Activator;
using MS.Libs.Infra.Utils.Extensions;
using MS.Libs.WebApi.Infrastructure.Extensions;
using MS.Libs.WebApi.Infrastructure.Filters;
using MS.Libs.WebApi.Infrastructure.Middlewares;
using MS.Services.Auth.Infra.IoC;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using Log = Serilog.Log;

namespace MS.Services.Auth.WebAPI.Infrastructure;
public class Startup
{
    protected AppSettings appSettings { get; set; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ExceptionFilter>();

        // Filtro de exceptios
        services.AddMvc(options =>
        {
            options.Filters.Add(typeof(ExceptionFilter));
            options.Filters.Add(new Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute(typeof(ResponseError<ErrorsModel>), 401));
        });

  
        // Add services to the container.
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWTContants.Key)),
                    ClockSkew = TimeSpan.Zero,
                };
            });

        // Binding model 
        services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true);

        appSettings = new AppSettings(Configuration);

        services.AddSingleton<AppSettings, AppSettings>();

        services.AddMemoryCache((options) =>{});

        // Register dependencys application
        App.Init<DependencyInjection>()
            .AddInfraSctructure(services, appSettings);

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MS.Services.Auth.WebAPI", Version = "v1" });


            //c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            //{
            //    Type = SecuritySchemeType.OAuth2,

            //    Flows = new OpenApiOAuthFlows
            //    {
            //        //https://localhost:7046/
            //        Password = new OpenApiOAuthFlow
            //        {
            //            TokenUrl = new Uri("https://localhost:7046/api/v1/auth/login"),
            //            Extensions = new Dictionary<string, IOpenApiExtension>
            //                    {
            //                        { "returnSecureToken", new OpenApiBoolean(true) },
            //                    },
            //        }

            //    }
            //}); 
            //c.OperationFilter<AuthorizeCheckOperationFilter>();

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                { SecuritySchemes.OAuthScheme, new List<string>() }
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                BearerFormat = "JWT",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "bearer",
                Description = "Please insert JWT token into field",
                Flows = new OpenApiOAuthFlows
                {
                    //https://localhost:7046/
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri("https://localhost:7046/api/v1/auth/login"),
                        Extensions = new Dictionary<string, IOpenApiExtension>
                                {
                                    { "TokenJWT", new OpenApiBoolean(true) },
                                },
                    }

                }
            });


            //c.RegisterSwaggerDefaultConfig(true);

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), includeControllerXmlComments: false);
        });

        services.AddSwaggerExamples();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseMiddleware<RequestResponseLoggingMiddleware>();

        app.UseMiddleware<AuthUnauthorizedMiddleware>();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseSwagger();
        // subir no local host 
        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = "";

            //c.OAuthClientId("swagger-ui");
            //c.OAuthClientSecret("swagger-ui-secret");
            //c.OAuthRealm("swagger-ui-realm");
            //c.OAuthAppName("Swagger UI");

            c.SwaggerEndpoint("/swagger/v1/swagger.json", "MS.Services.Auth.WebAPI");
        });

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}


internal static class SecuritySchemes
{
    public static OpenApiSecurityScheme BearerScheme(IConfiguration config) => new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Description = "Standard authorisation using the Bearer scheme. Example: \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                Scopes = new Dictionary<string, string>
                    {
                        { "Auth", "My Api" }
                    },
                TokenUrl = new System.Uri($"{config["TokenServerUrl"]}connect/token")
            }
        }
    };

    public static OpenApiSecurityScheme OAuthScheme => new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        },
        Scheme = "oauth2",
        Name = "Bearer",
        In = ParameterLocation.Header,

    };
}


public class AuthorizeCheckOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {


        var requiredScopes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                 .OfType<AuthorizeAttribute>()
                 .Select(attr => attr.Roles)
                 .Distinct();

        if (requiredScopes.Any())
        {

            var oAuthScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
            };

            operation.Security = new List<OpenApiSecurityRequirement>
             {
                 new OpenApiSecurityRequirement
                 {
                     [ oAuthScheme ] = requiredScopes.ToList()
                 }
             };

        }
    }
}