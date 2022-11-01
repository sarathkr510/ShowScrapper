using ErrorOr;
using MediatR;
using Rtl.TvMazeScrapper.Domain.Interfaces.Cache;

namespace Rtl.TvMazeScrapper.Domain.DTO;

public class GetShowsRequestDto: IRequest<ErrorOr<IEnumerable<ShowDto>>>//, IRequest<ShowDTO>
{
    public GetShowsRequestDto(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }

    public int Page { get; set; }
    public int PageSize { get; set; }

}

