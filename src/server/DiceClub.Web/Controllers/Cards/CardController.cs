using DiceClub.Services.Cards;
using DiceClub.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiceClub.Web.Controllers.Cards;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
// [Authorize]
public class CardController : BaseAuthController
{
    private readonly ImportService _importService;
    private readonly ImportMtgService _importMtgService;

    public CardController(ImportService importService, ImportMtgService importMtgService)
    {
        _importService = importService;
        _importMtgService = importMtgService;
    }
    
    [HttpPost]
    [Route("upload/format/cardcastle")]
    public async Task<IActionResult> UploadCardCastleCsv(IFormFile file)
    {

        using var ms = new MemoryStream();
        var tmpFile = Path.Join(Path.GetTempPath(), file.FileName);
        await file.CopyToAsync(ms);
        await System.IO.File.WriteAllBytesAsync(tmpFile, ms.ToArray());
        await _importService.ImportCardCastleCsv(tmpFile, GetUserId());
        return Ok();
    }

    [HttpPost]
    [Route("import/mtg")]
    public async Task<IActionResult> ImportMtgDatabase()
    {
        await Task.Run(() => _importMtgService.ImportMtgDatabase());
        return Ok();
    }
    
}