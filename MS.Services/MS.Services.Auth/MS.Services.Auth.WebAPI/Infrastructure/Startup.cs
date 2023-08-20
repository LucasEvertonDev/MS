﻿using MS.Libs.Core.Domain.Models.Error;
using MS.Libs.WebApi.HttpContainers;
using MS.Libs.WebApi.Infrastructure.Filters;
using MS.Services.Auth.Infra.IoC;

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
            ////options.Filters.Add(new Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute(typeof(ResponseDTO<ErrorsModel>), 400));
            ////options.Filters.Add(new Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute(typeof(ResponseDTO<ErrorsModel>), 401));
            options.Filters.Add(new Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute(typeof(ResponseDTO<ErrorsModel>), 500));
        });

        // pra usar o middleware que não é attributee
        services.AddHttpContextAccessor();
        // Add services to the container.
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        /// Register dependencys application
        Activator.CreateInstance<DependencyInjection>()
            .AddInfraSctructure(services, Configuration);
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

        //Swagger
        app.UseSwagger(options =>
        {
            options.SerializeAsV2 = true;
        });

        //SwaggerUI
        app.UseSwaggerUI(c =>
        {
            //c.DocExpansion(DocExpansion.List);
            //c.DocExpansion(DocExpansion.None);
        });

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
