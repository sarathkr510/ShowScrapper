using FluentValidation;
using Rtl.TvMazeScrapper.Domain.DTO;

namespace Rtl.TvMazeScrapper.Application.Queries.Show;

public class ShowsQueryValidator: AbstractValidator<GetShowsRequestDto>
{
    public ShowsQueryValidator()
    {
        RuleFor(x => x.Page).NotNull().NotEmpty().WithMessage("Page number is needed");
        RuleFor(x => x.PageSize).NotNull().NotEmpty();
    }
}