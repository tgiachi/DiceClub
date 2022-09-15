using Aurora.Api.Entities.Attributes;
using Aurora.Api.Entities.Impl.Seeds;
using Aurora.Api.Entities.Interfaces.Dao;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Entities.Cards;
using Microsoft.Extensions.Logging;
using Mtg.Collection.Manager.Database.Entities;
using ScryfallApi.Client;

namespace DiceClub.Services.DbSeeds;


[DbSeed(3)]
public class CardsSeed : AbstractDbSeed<Guid, ColorCardEntity>
{
    private readonly ScryfallApiClient _scryfallApiClient;
    private readonly CardSetDao _cardSetDao;
    public CardsSeed(ColorCardDao dao, ILogger<AbstractDbSeed<Guid, ColorCardEntity>> logger, ScryfallApiClient scryfallApiClient, CardSetDao cardSetDao) : base(dao, logger)
    {
        _scryfallApiClient = scryfallApiClient;
        _cardSetDao = cardSetDao;
    }

    public async override Task<bool> Seed()
    {
        var sets = await _scryfallApiClient.Sets.Get();

        foreach (var set in sets.Data)
        {
            await _cardSetDao.CreateIfNotExists(set.Code, set.Name, set.IconSvgUri != null ? set.IconSvgUri.ToString() : " ");
        }

        return true;
    }
}
