using Microsoft.EntityFrameworkCore;
using Rtl.TvMazeScrapper.Domain.Entity;

namespace Rtl.TvMazeScrapper.Infrastructure.Data.Mappings;

public static class ShowMap
{
    public static ModelBuilder MapShows(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Show>();

        // Key
        entity.HasKey(k => k.Id);

        // Index
        entity.HasIndex(i => i.Name)
            .IsUnique();

        // Relations
        entity.HasMany(p => p.Casts)
            .WithOne(o => o.Show)
            .HasForeignKey(f => f.ShowId);

        // Length
        entity.Property(p => p.Name).HasMaxLength(1024);

        return modelBuilder;
    }
}