using System.Net;
using System.Net.Http.Headers;
using System.Text;
using DiceClub.Services.Base;
using DiceClub.Services.Cards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DiceClub.Web.Controllers.Cards;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/cards/[controller]")]
[Authorize]
public class ExportController : BaseAuthController
{
    private readonly ExportCardService _exportCardService;

    public ExportController(ExportCardService exportCardService)
    {
        _exportCardService = exportCardService;
    }

    [HttpGet]
    [Route("format/deckbox")]
    public async Task<IActionResult> ImportFromMtg()
    {
        var csv = await _exportCardService.ExportCsvFile((GetUserId()));

    

          return File(csv, "application/octet-stream", "export.csv");;
    }
}