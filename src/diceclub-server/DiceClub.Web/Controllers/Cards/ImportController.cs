using DiceClub.Api.Data.Cards;
using DiceClub.Api.Data.Rest;
using DiceClub.Services;
using DiceClub.Services.Cards;
using Microsoft.AspNetCore.Mvc;

namespace DiceClub.Web.Controllers.Cards;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
public class ImportController : ControllerBase
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
        await _cardService.ImportCsv(tmpFile, CardCsvImportType.CardCastle ,Guid.Parse("1090f324-d788-40a5-a98a-6b24ee0cd001"));
        return RestResultObjectBuilder<string>.Create().Data("queued").Build();
    }
}