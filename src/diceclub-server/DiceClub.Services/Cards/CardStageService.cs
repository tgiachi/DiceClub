using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using DiceClub.Api.Data.Rest;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Entities.MtgCards;
using Microsoft.Extensions.Logging;
using ScryfallApi.Client;
using ScryfallApi.Client.Models;

namespace DiceClub.Services.Cards;

public class CardStageService : AbstractBaseService<CardStageService>
{
    private readonly MtgCardStageDao _mtgCardStageDao;
    private readonly MtgDumpDao _mtgDumpDao;
    private readonly MtgCardLanguageDao _mtgCardLanguageDao;
    private readonly CardService _cardService;
    private readonly ScryfallApiClient _scryfallApiClient;

    public CardStageService(IEventBusService eventBusService, ILogger<CardStageService> logger,
        MtgCardStageDao mtgCardStageDao, CardService cardService, ScryfallApiClient scryfallApiClient,
        MtgDumpDao mtgDumpDao, MtgCardLanguageDao mtgCardLanguageDao) : base(
        eventBusService, logger)
    {
        _mtgCardStageDao = mtgCardStageDao;
        _cardService = cardService;
        _scryfallApiClient = scryfallApiClient;
        _mtgDumpDao = mtgDumpDao;
        _mtgCardLanguageDao = mtgCardLanguageDao;
    }

    public async Task<bool> AddCardInStaging(CardStageAddRequest cardRequest, Guid userId)
    {
        var languageEntity = await _mtgCardLanguageDao.FindById(cardRequest.LanguageId);
        Logger.LogInformation("Adding card {Card} in stage area", cardRequest.CardName);

        var dumpCard = await _mtgDumpDao.QueryAsSingle(entities =>
            entities.Where(s =>
                s.CardName == cardRequest.CardName && s.Card.Set.ToLower() == cardRequest.SetCode.ToLower()));

        Card apiCard = null;

        var searchInLanguage = dumpCard.Card.ForeignNames.Select(s => s.Language.ToLower())
            .Contains(languageEntity.Name.ToLower());

        if (searchInLanguage)
        {
            var foreignName =
                dumpCard.Card.ForeignNames.FirstOrDefault(s => s.Language.ToLower() == languageEntity.Name.ToLower());
            var searchResult = await _scryfallApiClient.Cards.Search(foreignName.Name, 1,
                new SearchOptions { Mode = SearchOptions.RollupMode.Prints, IncludeMultilingual = true });

            if (searchResult.Data.Count > 0)
            {
                apiCard = searchResult.Data.FirstOrDefault(s => s.Set.ToLower() == cardRequest.SetCode.ToLower());
            }
        }

        if (apiCard == null)
        {
            var allCards = new List<Card>();
            var searchOptions = new SearchOptions
            {
                Mode = SearchOptions.RollupMode.Prints,
                IncludeMultilingual = true
            };
            Logger.LogWarning("Fallback for english language");
            var fallBackCards =
                await _scryfallApiClient.Cards.Search(dumpCard.CardName, 1, searchOptions);

            allCards.AddRange(fallBackCards.Data);

            var page = 1;
          
            while (fallBackCards.HasMore)
            {
                fallBackCards =
                    await _scryfallApiClient.Cards.Search(dumpCard.CardName, page,searchOptions);

                allCards.AddRange(fallBackCards.Data);
                page++;
            }

            apiCard = allCards.FirstOrDefault(s =>
                s.Name == cardRequest.CardName && s.Set.ToLower() == cardRequest.SetCode.ToLower());
        }

        if (apiCard == null)
        {
            return false;
        }

        await _mtgCardStageDao.Insert(new MtgCardStageEntity
        {
            LanguageId = languageEntity.Id,
            Quantity = cardRequest.Quantity,
            CardName = apiCard.PrintedName ?? apiCard.Name,
            ImageUrl = apiCard.ImageUris["normal"].ToString(),
            UserId = userId,
            IsAdded = false,
            IsFoil = cardRequest.IsFoil,
            ScryfallId = apiCard.Id.ToString()
        });

        return true;
    }

    public Task<List<MtgCardStageEntity>> FindAllByUser(Guid userId)
    {
        return _mtgCardStageDao.QueryAsList(entities => entities.Where(s => s.UserId == userId && s.IsAdded == false));
    }
}