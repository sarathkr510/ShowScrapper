using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Rtl.TvMazeScrapper.Api.Errors;

namespace Rtl.TvMazeScrapper.Api;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();
        
        return services;
    }
}