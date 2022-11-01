using Microsoft.EntityFrameworkCore;
using Rtl.TvMazeScrapper.Domain.DTO;
using Rtl.TvMazeScrapper.Domain.Entity;
using Rtl.TvMazeScrapper.Domain.Interfaces.Repository;
using Rtl.TvMazeScrapper.Domain.Settings;

namespace Rtl.TvMazeScrapper.Infrastructure.Data.Repositories
{
    public class ShowRepository : BaseRepository<Show>, IShowRepository<Show>
    {
        private readonly TvMazeDbContext _dbContext;
        private readonly PaginationSettings _paginationSettings;
        public ShowRepository(TvMazeDbContext dbContext, PaginationSettings paginationSettings) : base(dbContext)
        {
            _dbContext = dbContext;
            _paginationSettings = paginationSettings;
        }
        public async Task<List<Show>> GetShowsByPageNumber(GetShowsRequestDto queryDto)
        {
            var pageSize = queryDto.PageSize != default
                ? queryDto.PageSize
                : _paginationSettings.PageSize;

            var page = queryDto.Page != default
                ? queryDto.Page
                : _paginationSettings.DefaultPage;

            return await _dbContext.Show
                .Include(i => i.Casts)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
