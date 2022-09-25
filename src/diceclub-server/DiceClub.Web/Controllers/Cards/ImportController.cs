using DiceClub.Api.Data.Cards;
using DiceClub.Api.Data.Rest;
using DiceClub.Services;
using DiceClub.Services.Base;
using DiceClub.Services.Cards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiceClub.Web.Controllers.Cards;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
[Authorize]
public class ImportController : BaseAuthController
{
    private readonly MtgCardService _mtgCardService;
    private readonly CardService _cardService;


    public ImportController(MtgCardService mtgCardService, CardService cardService)
    {
        _mtgCardService = mtgCardService;
        _cardService = cardService;
    }

    [HttpGet]
    [Route("mtg")]
    public Task<RestResultObject<bool>> ImportFromMtg()
    {
        Task.Run(async () => { await _mtgCardService.ImportFromMtg(); });

        return Task.FromResult(RestResultObjectBuilder<bool>.Create().Data(true).Build());
    }
    
    [HttpPost]
    [Route("format/cardcastle")]
    public async Task<ActionResult<RestResultObject<string>>> UploadCardCastleCsv(IFormFile file)
    {
        using var ms = new MemoryStream();
        var tmpFile = Path.Join(Path.GetTempPath(), file.FileName);
        await file.CopyToAsync(ms);
        await System.IO.File.WriteAllBytesAsync(tmpFile, ms.ToArray());
        await _cardService.ImportCsv(tmpFile, CardCsvImportType.CardCastle ,GetUserId());
        return RestResultObjectBuilder<string>.Create().Data("queued").Build();
    }
}