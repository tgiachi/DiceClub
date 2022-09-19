using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using DiceClub.Api.Events.Cards;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Entities.Cards;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services.Cards;

public class CardStagingService : AbstractBaseService<CardStagingService>
{
    private readonly CardStagingDao _cardStagingDao;

    public CardStagingService(IEventBusService eventBusService, ILogger<CardStagingService> logger,
        CardStagingDao cardStagingDao) : base(eventBusService, logger)
    {
        _cardStagingDao = cardStagingDao;
    }

    public Task<List<CardStagingEntity>> FindStagingCardByUser(Guid userId)
    {
        return _cardStagingDao.FindStagingCardByUser(userId);
    }

    public async Task<Guid> AddCardToStaging(int mtgId, string language, bool isFoil, Guid userId)
    {
        var entity = await _cardStagingDao.Insert(new CardStagingEntity
        {
            Language = language,
            UserId = userId,
            IsFoil = isFoil,
            MtgId = mtgId,
        });

        return entity.Id;
    }

    public Task<bool> DeleteStagingCard(Guid id)
    {
        return _cardStagingDao.DeleteStagingCard(id);
    }

    public async Task<bool> CommitCardStaging(Guid id)
    {
        var stagingCard = await _cardStagingDao.FindById(id);

        await PublishEvent(new AddCardFromStagingEvent()
        {
            Language = stagingCard.Language,
            StagingId = stagingCard.Id
        });

        stagingCard.IsAdded = true;

        await _cardStagingDao.Update(stagingCard);

        return true;
    }

    public async Task<List<bool>> CommitAllCards(Guid userId)
    {
        var stagingCards = await _cardStagingDao.FindStagingCardByUser(userId);

        var result = new List<bool>();
        foreach (var card in stagingCards)
        {
            result.Add(await CommitCardStaging(card.Id));
        }

        return result;
    }
}