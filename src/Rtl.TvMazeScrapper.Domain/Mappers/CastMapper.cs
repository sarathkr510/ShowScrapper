using Rtl.TvMazeScrapper.Domain.Constants;
using Rtl.TvMazeScrapper.Domain.DTO;
using Rtl.TvMazeScrapper.Domain.Entity;

namespace Rtl.TvMazeScrapper.Domain.Mappers;

public static class CastMapper
{
    public static CastDTO Map(this Cast cast)
    {
        return cast != null ? new CastDTO
            {
                Id = cast.Id,
                Name = cast.Name,
                Birthday = cast.Birthday?.ToString(FormatConstants.DateFormat).Replace("/", "-")
            } : null;
    }
}