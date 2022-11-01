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

    
    var sqliteOptions = new SQLiteStorageOptions();
    builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        //.UseMemoryStorage(new MemoryStorageOptions { JobExpirationCheckInterval = TimeSpan.FromMinutes(10) })
        .UseSQLiteStorage("Filename=tvmaze-db;", sqliteOptions)
    );
    builder.Services.AddHangfireServer();
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

    app.UseHangfireDashboard();
    app.UseExceptionHandler("/api/error");
    //app.UseHangfireServer();  
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
    
}