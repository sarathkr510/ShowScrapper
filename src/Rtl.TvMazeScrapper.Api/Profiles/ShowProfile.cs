using AutoMapper;
using Rtl.TvMazeScrapper.Contracts.ViewModels.Request;
using Rtl.TvMazeScrapper.Domain.Constants;
using Rtl.TvMazeScrapper.Domain.DTO;
using Rtl.TvMazeScrapper.Domain.Entity;
using static System.Globalization.CultureInfo;

namespace Rtl.TvMazeScrapper.Api.Profiles;

public class ShowProfile: Profile
{
    public ShowProfile()
    {
        // Source -> Target
        CreateMap<GetShowsVm, GetShowsRequestDto>();
        

        CreateMap<Show, ShowDto>()
            .ForMember(d => d.Id,
                opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Name,
                opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Cast,
                opt => opt.MapFrom(s =>
                    s.Casts.OrderByDescending(b => b.Birthday).AsEnumerable().Select(cast => cast).ToList()));
        
        CreateMap<Cast, CastDTO>()
            .ForMember(d => d.Id,
                opt =>
                    opt.MapFrom(s => s.Id))
            .ForMember(d => d.Name,
                opt =>
                    opt.MapFrom(s => s.Name))
            .ForMember(dest => dest.Birthday,
                opt =>
                    opt.MapFrom(s => s.Birthday!.Value.ToString(FormatConstants.DateFormat, InvariantCulture)
                        .Replace("/", "-")));
        
    }
}