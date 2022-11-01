using Rtl.TvMazeScrapper.Domain.Entity;
using Rtl.TvMazeScrapper.Domain.Interfaces.Repository;

namespace Rtl.TvMazeScrapper.Infrastructure.Data.Repositories;

public class CastRepository : BaseRepository<Cast>, ICastRepository<Cast>
{
    public CastRepository(TvMazeDbContext dbContext) : base(dbContext) { }
}