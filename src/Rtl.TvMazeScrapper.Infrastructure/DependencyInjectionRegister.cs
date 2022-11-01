using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Rtl.TvMazeScrapper.Domain.Constants;
using Rtl.TvMazeScrapper.Domain.Entity;
using Rtl.TvMazeScrapper.Domain.Interfaces.Repository;
using Rtl.TvMazeScrapper.Domain.Interfaces.ServiceClients;
using Rtl.TvMazeScrapper.Domain.Interfaces.Services;
using Rtl.TvMazeScrapper.Domain.Services;
using Rtl.TvMazeScrapper.Domain.Settings;
using Rtl.TvMazeScrapper.Infrastructure.BackgroundServices;
using Rtl.TvMazeScrapper.Infrastructure.Data;
using Rtl.TvMazeScrapper.Infrastructure.Data.Repositories;
using Rtl.TvMazeScrapper.Infrastructure.Http;
using Rtl.TvMazeScrapper.Infrastructure.Http.Retry;

namespace Rtl.TvMazeScrapper.Infrastructure;

public static class DependencyInjectionRegister
{
   public static IServiceCollection AddInfrastructure(
      this IServiceCollection services,
      IConfiguration configuration)
   {
      #region Settings
            
      services.AddSingleton(configuration.BindSettings<PaginationSettings>(nameof(PaginationSettings)));
      services.AddSingleton(configuration.BindSettings<ScraperSettings>(nameof(ScraperSettings)));
      services.AddSingleton(configuration.BindSettings<RateLimitSettings>(nameof(RateLimitSettings)));
      
      #endregion
      
      #region Http client
      services.AddHttpClient(HttpClientConstants.THROTTLED_HTTP_CLIENT)
         .AddPolicyHandler(PolicyProvider.Get());
            
      #endregion

      #region DB
      services.AddDbContext<TvMazeDbContext>(options => options.UseSqlite(configuration.GetConnectionString(nameof(TvMazeDbContext))));
      #endregion

      #region Services
      services.AddTransient<IShowService, ShowService>();
      #endregion

      #region ServiceClients
      services.AddTransient<IScraperServiceClient, ScraperServiceClient>();
      #endregion

      #region Repositories
      services.AddTransient<IShowRepository<Show>, ShowRepository>();
      services.AddTransient<ICastRepository<Cast>, CastRepository>();
      #endregion

      #region Hosted services
      services.AddHostedService<DataIngestionService>();
      #endregion

      return services;
   }
   
   
}