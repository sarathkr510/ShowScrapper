using Rtl.TvMazeScrapper.Domain.DTO;

namespace Rtl.TvMazeScrapper.Domain.Interfaces.Services;

public interface IShowService
{
    Task<List<ShowDto>> GetShowsByPageNumber(GetShowsRequestDto requestDto);
}