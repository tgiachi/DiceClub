using System.Collections.Concurrent;
using System.Globalization;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using CsvHelper;
using CsvHelper.Configuration;
using Dasync.Collections;
using DiceClub.Api.Data.Cards;
using DiceClub.Api.Data.Cards.Mtg;
using DiceClub.Api.Events.WebSocket;
using DiceClub.Api.Utils;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Entities.Account;
using DiceClub.Database.Entities.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mtg.Collection.Manager.Database.Entities;
using ScryfallApi.Client;
using ScryfallApi.Client.Models;

namespace DiceClub.Services.Cards
{
    public class ImportService : AbstractBaseService<ImportService>
    {
        private readonly ScryfallApiClient _scryfallApiClient;
        private readonly ColorCardDao _colorCardDao;
        private readonly ColorsDao _colorsDao;
        private readonly CardTypeDao _cardTypeDao;
        private readonly RarityDao _rarityDao;
        private readonly CardsDao _cardsDao;
        private readonly CardSetDao _cardSetDao;
        private readonly CardLegalityDao _cardLegalityDao;
        private readonly CardLegalityTypeDao _cardLegalityTypeDao;
        private readonly CardCardLegalityDao _cardCardLegalityDao;
        private readonly CreatureTypeDao _creatureTypeDao;
        private readonly CardService _cardService;

        private readonly ImportMtgService _importMtgService;


        public ImportService(IEventBusService eventBusService,
            ILogger<ImportService> logger,
            ScryfallApiClient scryfallApiClient,
            ColorCardDao colorCardDao,
            ColorsDao colorsDao,
            CardTypeDao cardTypeDao,
            RarityDao rarityDao,
            CardsDao cardsDao,
            CardSetDao cardSetDao,
            CardLegalityDao cardLegalityDao,
            ImportMtgService importMtgService, CardLegalityTypeDao cardLegalityTypeDao, CardCardLegalityDao cardCardLegalityDao, CreatureTypeDao creatureTypeDao, CardService cardService) : base(eventBusService, logger)
        {
            _scryfallApiClient = scryfallApiClient;
            _colorCardDao = colorCardDao;
            _colorsDao = colorsDao;
            _cardTypeDao = cardTypeDao;
            _rarityDao = rarityDao;
            _cardsDao = cardsDao;
            _cardSetDao = cardSetDao;
            _importMtgService = importMtgService;
            _cardLegalityTypeDao = cardLegalityTypeDao;
            _cardCardLegalityDao = cardCardLegalityDao;
            _creatureTypeDao = creatureTypeDao;
            _cardService = cardService;
            _cardLegalityDao = cardLegalityDao;
        }

        public async Task ImportCardCastleCsv(string fileName, Guid clubUserId)
        {
            var records = await ImportCsv(fileName);
            var cards = await SearchCardsFromCsv(records);

            await ImportColors(cards);
            await ImportCardTypes(cards);
            await ImportCreatureTypes(cards);
            await ImportRarities(cards);
            await ImportSets(cards);
            await ImportLegalities(cards);
            await AddCards(cards, clubUserId);
        }

        public async Task<List<CardCastleRecord>> ImportCsv(string fileName)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };
            using var reader = new StreamReader(fileName);
            using var csv = new CsvReader(reader, config);

            await PublishEvent(new DiagnosticMessageEvent()
            {
                Message = $"Starting parsing file '{fileName}' ",
                Type = "info"
            });

            var records = csv.GetRecords<CardCastleRecord>().ToList();

            await PublishEvent(new DiagnosticMessageEvent()
            {
                Message = $"Parsed {records.Count} cards",
                Type = "info"
            });

            return records;
        }

        public async Task<List<Card>> SearchCardsFromCsv(List<CardCastleRecord> records)
        {
            var cards = new ConcurrentBag<Card>();


            await records.ParallelForEachAsync(async (record, i) =>
            {
                if (string.IsNullOrEmpty(record.Name)) return;


                try
                {
                    Logger.LogInformation("Searching card {Current}/{Count} - {Name}", i, records.Count, record.Name);

                    MtgCard foundCard = null;
                    if (record.MultiverseId != null)
                    {
                        foundCard = await _importMtgService.FindCardByMultiverseId((int)record.MultiverseId);
                    }
                    else
                    {
                        foundCard = await _importMtgService.FindCardByName(record.Name);
                    }

                    if (foundCard == null)
                    {
                        foundCard = await _importMtgService.FindByJsonId(record.JsonId);
                    }

                    if (foundCard == null)
                    {
                        var fallBackCards =
                            await _scryfallApiClient.Cards.Search(record.Name, 1, SearchOptions.CardSort.Name);
                        if (fallBackCards.Data.Count > 0)
                        {
                            try
                            {
                                var fallBackCard =
                                    fallBackCards.Data.FirstOrDefault(card => card.Id == Guid.Parse(record.JsonId));

                                if (fallBackCard != null)
                                {
                                    cards.Add(fallBackCard);
                                    Logger.LogInformation("Added fallback card {Card} - {CardName}", fallBackCard.Name,
                                        record.Name);
                                    return;
                                }
                            }
                            catch
                            {
                                var guidErrorCard = fallBackCards.Data.FirstOrDefault(s => s.Name == record.Name);
                                if (guidErrorCard != null)
                                {
                                    cards.Add(guidErrorCard);
                                    Logger.LogInformation("Added fallback card {Card} - {CardName}", guidErrorCard.Name,
                                        record.Name);
                                }
                            }
                        }
                    }

                    if (foundCard == null)
                    {
                        Logger.LogWarning("Card {CardName} not found in any db:", record.Name);
                        return;
                    }

                    if (foundCard.ForeignNames != null &&
                        foundCard.ForeignNames.Count(s => s.Language == "Italian") > 0)
                    {
                        var italianCard = foundCard.ForeignNames.Where(s => s.Language == "Italian").Select(s => s)
                            .FirstOrDefault();
                        Logger.LogInformation("Found italian version of {CardName} [{ItalianName}]", foundCard.Name,
                            italianCard.Name);

                        var skCard = await _scryfallApiClient.Cards.Search(italianCard.Name, 1, new SearchOptions
                        {
                            IncludeMultilingual = true
                        });

                        if (skCard.Data.Count > 0)
                        {
                            cards.Add(skCard.Data.FirstOrDefault());
                        }
                    }
                    else
                    {
                        Logger.LogInformation("Italian card not found, fallback to english");
                        var englishCard = await _scryfallApiClient.Cards.Search(foundCard.Name, 1, new SearchOptions
                        {
                            Sort = SearchOptions.CardSort.Name,
                        });

                        if (englishCard.Data.Count > 0)
                        {
                            cards.Add(englishCard.Data.FirstOrDefault());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("Error during parse card {Card}: {Ex}", record.Name, ex);
                }
            }, 20);

            return cards.ToList();
        }

        public async Task ImportLegalities(List<Card> cards)
        {
            var legalities = cards.SelectMany(s => s.Legalities).DistinctBy(s => s.Key).Select(s => s.Key).ToList();
            var legalitiesTypes = cards.SelectMany(s => s.Legalities).DistinctBy(s => s.Value).Select(s => s.Value).ToList();


            foreach (var cl in legalities)
            {
                await _cardLegalityDao.CreateIfNotExists(cl);
            }

            foreach (var legalitiesType in legalitiesTypes)
            {
                await _cardLegalityTypeDao.CreateIfNotExists(legalitiesType);
            }
            
        }

        public async Task ImportCreatureTypes(List<Card> cards)
        {
            var creaturesTypes = cards.Where(s => s.TypeLine.ToLower().StartsWith("creature"))
                .Select(s => s.TypeLine.Split('—')[1].Trim()).ToList();


            foreach (var creatureType in creaturesTypes)
            {
                await _creatureTypeDao.AddIfNotExists(creatureType);
            }
            
        }

        public async Task ImportSets(List<Card> cards)
        {
            var sets = await _scryfallApiClient.Sets.Get();
            
            foreach (var set in sets.Data)
            {
                await _cardSetDao.CreateIfNotExists(set.Code, set.Name, set.IconSvgUri.ToString());
            }
        }

        public async Task ImportColors(List<Card> cards)
        {
            await PublishEvent(new DiagnosticMessageEvent()
            {
                Message = $"Adding colors",
                Type = "info"
            });
            var colors = cards.Where(s => s != null && s.Colors != null).SelectMany(s => s.Colors).DistinctBy(s => s)
                .ToList();

            foreach (var color in colors)
            {
                await _colorsDao.AddIfNotExists(color);
            }
        }

        private async Task ImportCardTypes(List<Card> cards)
        {
            var types = cards.DistinctBy(s => s.TypeLine).Select(s => s.TypeLine.Split('—')[0].Trim())
                .DistinctBy(s => s).Select(s => s).ToList();

            foreach (var type in types)
            {
                await _cardTypeDao.AddCardTypeIfNotExists(type);
            }
        }

        public async Task ImportRarities(List<Card> cards)
        {
            var types = cards.DistinctBy(s => s.Rarity).DistinctBy(s => s).Select(s => s.Rarity).ToList();

            foreach (var type in types)
            {
                await _rarityDao.AddCardTypeIfNotExists(type);
            }
        }



        public async Task AddCards(List<Card> cards, Guid userId)
        {
            await cards.ParallelForEachAsync(async (card, index) =>
            {
                await _cardService.AddCard(card, userId, (int)index);
            }, 30);
        }
    }
}