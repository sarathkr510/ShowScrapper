using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rtl.TvMazeScrapper.Application.Commands.Show;
using Rtl.TvMazeScrapper.Contracts.ViewModels.Request;
using Rtl.TvMazeScrapper.Domain.DTO;
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
    public ShowController(ILogger<ShowController> logger, IShowService showService, IMapper mapper, ISender sender)
    {
        _logger = logger;
        _showService = showService;
        _mapper = mapper;
        _sender = sender;
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
    #if DEBUG
        [ApiExplorerSettings(IgnoreApi = false)]
    #else
        [ApiExplorerSettings(IgnoreApi = true)]
    #endif 
    [HttpGet]
    [Route("UpdateDatabase")]
    public IActionResult UpdateShowDb()
    {
        //Recurring Job - this job is executed many times on the specified cron schedule
       // RecurringJob.AddOrUpdate(() => _scraperServiceClient.ExecuteScraping(), Cron.Hourly);
       var command = new ShowCommandRequest();
       _sender.Send(command);

        return Ok(new { message = "Backend api has been called, to update the database" } );
    }
    
}