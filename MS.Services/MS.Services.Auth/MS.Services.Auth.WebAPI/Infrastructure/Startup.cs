using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MS.Libs.Core.Domain.Constants;
using MS.Libs.Core.Domain.Infra.AppSettings;
using MS.Libs.Core.Domain.Infra.Claims;
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
using System.Collections.ObjectModel;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
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
            options.Filters.Add(new Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute(typeof(ErrorsModel), 401));
        });

        // pra usar o middleware que não é attributee
        services.AddHttpContextAccessor();
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

            c.RegisterSwaggerDefaultConfig(true);

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


public static class Teste
{
    public static async Task<string> GetRawBodyAsync(
       this HttpRequest request,
       Encoding encoding = null)
    {
        if (!request.Body.CanSeek)
        {
            // We only do this if the stream isn't *already* seekable,
            // as EnableBuffering will create a new stream instance
            // each time it's called
            request.EnableBuffering();
        }

        var reader = new StreamReader(request.Body, encoding ?? Encoding.UTF8);

        var body = await reader.ReadToEndAsync().ConfigureAwait(false);

        request.Body.Position = 0;

        return body;
    }

}