using Microsoft.EntityFrameworkCore;
using Rtl.TvMazeScrapper.Domain.Entity;
using Rtl.TvMazeScrapper.Infrastructure.Data.Mappings;

namespace Rtl.TvMazeScrapper.Infrastructure.Data;

public class TvMazeDbContext: DbContext
{
    public TvMazeDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Show> Show { get; set; }
    public DbSet<Cast> Cast { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) 
    {
        builder.MapShows();
        builder.MapCasts();
    }
}