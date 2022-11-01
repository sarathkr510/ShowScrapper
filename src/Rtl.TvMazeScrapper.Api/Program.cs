using Hangfire;
using Hangfire.SQLite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Rtl.TvMazeScrapper.Api;
using Rtl.TvMazeScrapper.Application;
using Rtl.TvMazeScrapper.Domain.Settings;
using Rtl.TvMazeScrapper.Infrastructure;
using Rtl.TvMazeScrapper.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    
    //builder.Services.AddControllers();

    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddControllers(options =>
    {
        var cacheProfiles = builder.Configuration
            .GetSection("CacheProfiles")
            .GetChildren();
        foreach (var cacheProfile in cacheProfiles)
        {
            options.CacheProfiles
                .Add(cacheProfile.Key,
                    cacheProfile.Get<CacheProfile>());
        }
    });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "RTL TvMaze Scrapper", Version = "v1.0.0" });
        });
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler("/api/error");
    //app.UseHangfireServer();  
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
    
}