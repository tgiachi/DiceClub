using System.Text.Json;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using Dasync.Collections;
using DiceClub.Api.Data.Cards.Mtg;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Entities.Cards;
using Microsoft.Extensions.Logging;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Service;

namespace DiceClub.Services.Cards;

public class ImportMtgService : AbstractBaseService<ImportMtgService>
{
    private readonly ICardService _cardService;
    private readonly MtgDao _mtgDao;

    public ImportMtgService(IEventBusService eventBusService, ILogger<ImportMtgService> logger,
        MtgServiceProvider mtgServiceProvider, MtgDao mtgDao) : base(eventBusService, logger)
    {
        _cardService = mtgServiceProvider.GetCardService();
        _mtgDao = mtgDao;
    }

    public Task<List<MtgCard>> FindCardFullText(string text)
    {
        return Task.FromResult<List<MtgCard>>(null);
    }

    public async Task<MtgCard> FindByJsonId(string jsonId)
    {
        var entity = await _mtgDao.QueryAsSingle(entities => entities.Where(s => s.Card.Id == jsonId));

        if (entity != null)
        {
            return entity.Card;
        }

        return null;
    }

    public async Task<MtgCard> FindCardByName(string name)
    {
        var entity = await _mtgDao.QueryAsSingle(entities => entities.Where(s => s.CardName == name));

        if (entity != null)
        {
            return entity.Card;
        }

        return null;
    }

    public async Task<MtgCard> FindCardByMultiverseId(int id)
    {
        var entity = await _mtgDao.QueryAsSingle(entities => entities.Where(s => s.MultiverseId == id));

        if (entity != null)
        {
            return entity.Card;
        }

        return null;
    }

    public async Task ImportMtgDatabase()
    {
        var page = 1;
        var maxPage = 0;

        var tmpDirectory = Path.GetTempPath();

        var firstRequest = await _cardService.Where(s => s.Page, 1).AllAsync();
        maxPage = firstRequest.PagingInfo.TotalPages;
        //  page++;

        // await AddCards(firstRequest.Value.ToMtgCards());

        while (page < maxPage)
        {
            var cards = new List<MtgCard>();
            var pageFileName = Path.Join(tmpDirectory, $"mtg_{page}.json");

            if (File.Exists(pageFileName))
            {
                Logger.LogInformation("Loading from cache page {Page}/{PageMax}", page, maxPage);
                cards.AddRange(JsonSerializer.Deserialize<List<MtgCard>>(await File.ReadAllTextAsync(pageFileName)) ??
                               new());
            }
            else
            {
                Logger.LogInformation("Downloading {Page}/{PageMax}", page, maxPage);
                var searchResult = await _cardService.Where(s => s.Page, page).AllAsync();
                cards.AddRange(searchResult.Value.ToMtgCards());
                await File.WriteAllTextAsync(pageFileName, JsonSerializer.Serialize(cards));
            }

            await AddCards(cards);
            page++;
        }
    }

    private Task AddCards(List<MtgCard> cards)
    {
        return cards.ParallelForEachAsync(async card =>
        {
            var exists = await _mtgDao.FindByIdAndName(card.MultiverseId == null ? 0 : int.Parse(card.MultiverseId), card.Name);

            if (exists == null)
            {
                await _mtgDao.Insert(new MtgEntity
                {
                    MultiverseId = card.MultiverseId == null ? 0 : int.Parse(card.MultiverseId),
                    CardName = card.Name,
                    Card = card,
                    ForeignNames = string.Join(',', card.ForeignNames.Select(s => s.Name))
                });

                Logger.LogInformation("Adding {CardName}", card.Name);
            }
        }, 10);
       
    }
}