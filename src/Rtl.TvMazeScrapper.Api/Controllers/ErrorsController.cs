using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Rtl.TvMazeScrapper.Api.Controllers;
[Route("/api/error")]
public class ErrorsController : ControllerBase
{
    [HttpGet]
    public IActionResult ErrorActionResult()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Problem();
    }
}