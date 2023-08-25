﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MS.Libs.Core.Domain.Constants;
using MS.Libs.Core.Domain.Infra.AppSettings;
using MS.Libs.Core.Domain.Models.Error;
using MS.Libs.Infra.Utils.Activator;
using MS.Libs.WebApi.Infrastructure.Extensions;
using MS.Libs.WebApi.Infrastructure.Filters;
using MS.Services.Auth.Infra.Data.Contexts;
using MS.Services.Auth.Infra.IoC;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;

namespace MS.Services.Auth.WebAPI.Infrastructure;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Filtro de exceptios
        services.AddMvc(options =>
        {
            options.Filters.Add(typeof(ExceptionFilter));
            options.Filters.Add(new Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute(typeof(ErrorsModel), 500));
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

        var configurations = new AppSettings(Configuration);

        services.AddSingleton<AppSettings, AppSettings>();

        services.AddMemoryCache((options) =>{});

        // Register dependencys application
        App.Init<DependencyInjection>()
            .AddInfraSctructure(services, configurations);

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MS.Services.Products.WebAPI", Version = "v1" });

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
