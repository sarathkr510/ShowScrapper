using AutoMapper;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rtl.TvMazeScrapper.Contracts.ViewModels.Request;
using Rtl.TvMazeScrapper.Domain.DTO;
using Rtl.TvMazeScrapper.Domain.Extensions;
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
        var requestModel = _mapper.Map<GetShowsRequestDto>(request);
        
        var result = await _sender.Send(requestModel);

        return result.Match(ShowDTO => Ok(result.Value),
            errors => Problem(errors)
        );
        

    }
}