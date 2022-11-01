using Rtl.TvMazeScrapper.Domain.DTO;
using Rtl.TvMazeScrapper.Domain.Entity;

namespace Rtl.TvMazeScrapper.Domain.Mappers;

public static class ShowMapper
{
    public static ShowDTO Map(this Show show)
    {
        return show != null
            ?
            new ShowDTO
            {
                Id = show.Id,
                Name = show.Name,
                Cast = show.Casts.OrderByDescending(d => d.Birthday).Select(s => s.Map()).ToList()
            }
            : 
            null;
    }
}