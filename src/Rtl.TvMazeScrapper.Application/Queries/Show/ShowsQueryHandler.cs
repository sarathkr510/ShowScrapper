using AutoMapper;
using ErrorOr;
using MediatR;
using Rtl.TvMazeScrapper.Domain.DTO;
using Rtl.TvMazeScrapper.Domain.Interfaces.Services;

namespace Rtl.TvMazeScrapper.Application.Queries.Show;

public class ShowsQueryHandler: IRequestHandler<GetShowsRequestDto, ErrorOr<IEnumerable<ShowDto>>>
{
    private readonly IShowService _showService;
    private readonly IMapper _mapper;

    public ShowsQueryHandler(IShowService showService, IMapper mapper)
    {
        _showService = showService;
        _mapper = mapper;
    }

    public async Task<ErrorOr<IEnumerable<ShowDto>>> Handle(GetShowsRequestDto request, CancellationToken cancellationToken) =>
        await _showService.GetShowsByPageNumber(request);
}