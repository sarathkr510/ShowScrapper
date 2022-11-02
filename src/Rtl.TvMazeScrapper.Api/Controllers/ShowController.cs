using AutoMapper;
using ErrorOr;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rtl.TvMazeScrapper.Contracts.ViewModels.Request;
using Rtl.TvMazeScrapper.Domain.DTO;
using Rtl.TvMazeScrapper.Domain.Extensions;
using Rtl.TvMazeScrapper.Domain.Interfaces.ServiceClients;
using Rtl.TvMazeScrapper.Domain.Interfaces.Services;

namespace Rtl.TvMazeScrapper.Api.Controllers;

[ApiController]
[Route("/")]
public class ShowController: ApiController
{
    private readonly ILogger<ShowController> _logger;
    private readonly IShowService _showService;
    private readonly IMapper _mapper;
    private readonly ISender _sender;
    private readonly IScraperServiceClient _scraperServiceClient;
    public ShowController(ILogger<ShowController> logger, IShowService showService, IMapper mapper, ISender sender,
        IScraperServiceClient scraperServiceClient)
    {
        _logger = logger;
        _showService = showService;
        _mapper = mapper;
        _sender = sender;
        _scraperServiceClient = scraperServiceClient;
    }

    [HttpPost, Route("GetShows")]
    [ResponseCache(CacheProfileName = "Cache2Mins")]
    public async Task<IActionResult> GetShows([FromQuery] GetShowsVm request)
    {
        
        
        var result = await _sender.Send(_mapper.Map<GetShowsRequestDto>(request));

        return result.Match(ShowDTO => Ok(result.Value),
            errors => Problem(errors)
        );
        

    }
    
    [HttpGet]
    [Route("UpdateDatabase")]
    public IActionResult UpdateDatabase()
    {
        //Recurring Job - this job is executed many times on the specified cron schedule
        RecurringJob.AddOrUpdate(() => _scraperServiceClient.ExecuteScraping(), Cron.Hourly);

        return Ok(new { message = "Backend api has been called, to update the database" } );
    }
}