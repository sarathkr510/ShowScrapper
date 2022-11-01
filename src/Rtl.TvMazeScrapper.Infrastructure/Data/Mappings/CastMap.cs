using Microsoft.EntityFrameworkCore;
using Rtl.TvMazeScrapper.Domain.Entity;

namespace Rtl.TvMazeScrapper.Infrastructure.Data.Mappings;

public static class CastMap
{
    public static ModelBuilder MapCasts(this ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Cast>();

        // Key
        entity.HasKey(k => k.Id);

        // Index
        entity.HasIndex(i => new { i.ShowId, i.Name })
            .IsUnique();

        // Relations
        entity.HasOne(p => p.Show)
            .WithMany(o => o.Casts)
            .HasForeignKey(f => f.ShowId);

        // Length
        entity.Property(p => p.Name).HasMaxLength(1024);

        return modelBuilder;
    }
}