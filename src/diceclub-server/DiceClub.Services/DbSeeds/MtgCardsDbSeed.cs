using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Attributes;
using Aurora.Api.Entities.Impl.Seeds;
using Aurora.Api.Entities.Interfaces.Dao;
using DiceClub.Api.Data.Mtg;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Entities.MtgCards;
using Microsoft.Extensions.Logging;
using ScryfallApi.Client;

namespace DiceClub.Services.DbSeeds
{
    [DbSeed(1)]
    public class MtgCardsDbSeed : AbstractDbSeed<Guid, MtgCardEntity>
    {
        private readonly MtgCardSetDao _mtgCardSetDao;
        private readonly MtgCardRarityDao _mtgCardRarityDao;
        private readonly MtgCardColorDao _mtgCardColorDao;
        private readonly ScryfallApiClient _scryfallApiClient;
        private readonly MtgCardLegalityDao _mtgCardLegalityDao;
        private readonly MtgCardLegalityTypeDao _mtgCardLegalityTypeDao;
        private readonly MtgCardLanguageDao _mtgCardLanguageDao;

        private readonly Dictionary<string, string> _colors = new()
        {
            { "W", "White" },
            { "U", "Blue" },
            { "B", "Black" },
            { "R", "Red" },
            { "G", "Green" },
        };

        private readonly Dictionary<string, string> _languages = new()
        {
            { "English", "en" },
            { "Italian", "it" },
            { "Spanish", "es" },
            { "Japanese", "ja" },
            { "Chinese", "cn" }
        };

        public MtgCardsDbSeed(MtgCardDao dao, ILogger<AbstractDbSeed<Guid, MtgCardEntity>> logger,
            MtgCardColorDao mtgCardColorDao, MtgCardSetDao mtgCardSetDao, MtgCardRarityDao mtgCardRarityDao,
            ScryfallApiClient scryfallApiClient, MtgCardLegalityDao mtgCardLegalityDao,
            MtgCardLegalityTypeDao mtgCardLegalityTypeDao, MtgCardLanguageDao mtgCardLanguageDao) : base(dao, logger)
        {
            _mtgCardColorDao = mtgCardColorDao;
            _mtgCardSetDao = mtgCardSetDao;
            _mtgCardRarityDao = mtgCardRarityDao;
            _scryfallApiClient = scryfallApiClient;
            _mtgCardLegalityDao = mtgCardLegalityDao;
            _mtgCardLegalityTypeDao = mtgCardLegalityTypeDao;
            _mtgCardLanguageDao = mtgCardLanguageDao;
        }

        public override async Task<bool> Seed()
        {
            await CheckSets();
            await CheckColors();
            await CheckLegalities();
            await CheckLanguages();
            return true;
        }

        private async Task CheckSets()
        {
            _logger.LogInformation("Checking sets");
            var sets = await _scryfallApiClient.Sets.Get();

            _logger.LogInformation("Checking set: {Count}", sets.Data.Count);
            foreach (var set in sets.Data)
            {
                await _mtgCardSetDao.InsertIfNotExists(set.Code, set.Name, set.IconSvgUri.ToString(), set.card_count);
            }
        }

        private async Task CheckColors()
        {
            _logger.LogInformation("Checking colors");
            foreach (var color in _colors)
            {
                await _mtgCardColorDao.InsertIfNotExists(color.Key, color.Value,
                    $"https://c2.scryfall.com/file/scryfall-symbols/card-symbols/{color.Key}.svg");
            }
        }

        private async Task CheckLegalities()
        {
            var card = await _scryfallApiClient.Cards.GetRandom();
            foreach (var legality in card.Legalities)
            {
                await _mtgCardLegalityDao.InsertIfNotExists(legality.Key);
                await _mtgCardLegalityTypeDao.InsertIfNotExists(legality.Value);
            }
        }

        private async Task CheckLanguages()
        {
            foreach (var language in _languages)
            {
                await _mtgCardLanguageDao.InsertIfNotExists(language.Key, language.Value);
            }
        }
    }
}