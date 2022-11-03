using Rtl.TvMazeScrapper.Domain.DTO;
using Rtl.TvMazeScrapper.Domain.Entity;
using Rtl.TvMazeScrapper.Domain.Entity.Base;
using Rtl.TvMazeScrapper.Domain.Interfaces.Repository.Base;

namespace Rtl.TvMazeScrapper.Domain.Interfaces.Repository;

public interface IShowRepository<T> : IBaseRepository<T> where T : EntityBase
{
    Task<List<Show>> GetShowsByPageNumber(GetShowsRequestDto queryDto);
} 