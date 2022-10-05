using System.Globalization;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using CsvHelper;
using DiceClub.Api.Data.Csv;
using DiceClub.Database.Entities.MtgCards;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services.Cards;

public class ExportCardService : AbstractBaseService<ExportCardService>
{
    private readonly CardService _cardService;
    public ExportCardService(IEventBusService eventBusService, ILogger<ExportCardService> logger, CardService cardService) : base(eventBusService, logger)
    {
        _cardService = cardService;
    }
    
    

    public async Task<byte[]> ExportCsvFile(Guid userId)
    {
        var cards = await _cardService.ExportCardsByUserId(userId);

        var csvRecords = cards.Select(s => new DeckBoxExportRow()
        {
            Count = 1,
            TradeList = 0,
            Condition = "Near Mint",
            Foil = "",
            Name = s.Name,
            CardNumber = s.CollectorNumber?.ToString() ?? "0",
            Language = s.Language.Name,
            MyPrice = "$0"
        });

        using var mStream = new MemoryStream();
        using var tWriter = new StreamWriter(mStream);
        using var csv = new CsvWriter(tWriter, CultureInfo.InvariantCulture);
        await csv.WriteRecordsAsync(csvRecords);
        var streamReader = new StreamReader(mStream);
        return  mStream.ToArray();
        
    }


}