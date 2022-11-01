using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rtl.TvMazeScrapper.Domain.Interfaces.ServiceClients;

namespace Rtl.TvMazeScrapper.Infrastructure.BackgroundServices;

//Background Service instead IHostedService
public class DataIngestionService: BackgroundService
{
    private readonly IServiceProvider _provider;
    

    public DataIngestionService(IServiceProvider provider)
    {
        _provider = provider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        await Ingest();
    }

    private async Task Ingest()
    {
        using var scope = _provider.CreateScope();
        var scraperServiceClient = scope.ServiceProvider.GetRequiredService<IScraperServiceClient>();
        await scraperServiceClient.ExecuteScraping();
        
    }

}