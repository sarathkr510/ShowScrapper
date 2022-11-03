using AutoMapper;
using Rtl.TvMazeScrapper.Domain.DTO;
using Rtl.TvMazeScrapper.Domain.Entity;
using Rtl.TvMazeScrapper.Domain.Interfaces.Repository;
using Rtl.TvMazeScrapper.Domain.Interfaces.Services;

namespace Rtl.TvMazeScrapper.Infrastructure.Services;

public class ShowService : IShowService
{
    private readonly IShowRepository<Show> _showRepository;
    private readonly IMapper _mapper;
    public ShowService(IShowRepository<Show> showRepository, IMapper mapper)
    {
        _showRepository = showRepository;
        _mapper = mapper;
    }

    public async Task<List<ShowDto>> GetShowsByPageNumber(GetShowsRequestDto queryDto)
    {
        return (await _showRepository.GetShowsByPageNumber(queryDto)).AsEnumerable()
            .Select(s => _mapper.Map<ShowDto>(s)).ToList();
        
    }

}