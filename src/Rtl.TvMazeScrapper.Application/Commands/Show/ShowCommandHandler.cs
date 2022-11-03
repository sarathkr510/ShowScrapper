using Hangfire;
using MediatR;
using Rtl.TvMazeScrapper.Domain.Interfaces.ServiceClients;

namespace Rtl.TvMazeScrapper.Application.Commands.Show;

public class ShowCommandHandler: IRequestHandler<ShowCommandRequest>
{
    private readonly IScraperServiceClient _scraperServiceClient;

    public ShowCommandHandler(IScraperServiceClient scraperServiceClient)
    {
        _scraperServiceClient = scraperServiceClient;
    }
    public  Task<Unit> Handle(ShowCommandRequest request, CancellationToken cancellationToken)
    {
        BackgroundJob.Enqueue(() => _scraperServiceClient.ExecuteScraping());
        return Unit.Task;
    }
}