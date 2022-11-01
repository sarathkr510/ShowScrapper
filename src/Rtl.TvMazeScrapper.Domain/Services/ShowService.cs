using AutoMapper;
using Rtl.TvMazeScrapper.Domain.DTO;
using Rtl.TvMazeScrapper.Domain.Entity;
using Rtl.TvMazeScrapper.Domain.Interfaces.Repository;
using Rtl.TvMazeScrapper.Domain.Interfaces.Services;

namespace Rtl.TvMazeScrapper.Domain.Services;

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
        /*var testingS = (await _showRepository.GetShowsByPageNumber(queryDTO)).AsEnumerable()
            .Select(s => _mapper.Map<ShowDTO>(s));*/
        return (await _showRepository.GetShowsByPageNumber(queryDto)).AsEnumerable()
            .Select(s => _mapper.Map<ShowDto>(s)).ToList();
        
    }

}