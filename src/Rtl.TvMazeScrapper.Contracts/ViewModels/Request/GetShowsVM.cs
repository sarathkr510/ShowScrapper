using System.ComponentModel.DataAnnotations;

namespace Rtl.TvMazeScrapper.Contracts.ViewModels.Request;

public record GetShowsVm
{
    
    [Range(1, int.MaxValue, ErrorMessage = "Invalid page.")]
    public int Page { get; set; }
    [Range(1, 10, ErrorMessage = "Invalid page size.")]
    public int PageSize { get; set; }
}