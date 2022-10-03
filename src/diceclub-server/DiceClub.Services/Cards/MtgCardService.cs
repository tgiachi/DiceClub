using System.Collections.Concurrent;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using Dasync.Collections;
using DiceClub.Api.Data.Mtg;
using DiceClub.Api.MethodEx.Cards;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Entities.MtgCards;
using Microsoft.Extensions.Logging;
using MtgApiManager.Lib.Service;
using Newtonsoft.Json;

namespace DiceClub.Services.Cards
{
    public class MtgCardService : AbstractBaseService<MtgCardService>
    {
        private readonly MtgDumpDao _mtgDumpDao;
        private readonly MtgServiceProvider _mtgServiceProvider;
        private readonly int _numThreads = 1;
        private readonly int _numImportThreads = 10;

        public MtgCardService(IEventBusService eventBusService, ILogger<MtgCardService> logger, MtgDumpDao mtgDumpDao,
            MtgServiceProvider mtgServiceProvider) : base(eventBusService, logger)
        {
            _mtgDumpDao = mtgDumpDao;
            _mtgServiceProvider = mtgServiceProvider;
        }

        public async Task ImportFromMtg()
        {
            var totalPages = 0;
            var downloadedCards = new ConcurrentBag<MtgCard>();
            var tempPath = Path.Join(Path.GetTempPath(), "mtg_dump");
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }

            await PublishEvent(NotificationEventBuilder.Create().Broadcast().Title("Import MTG")
                .Message("Importing cards").Progress(1, totalPages).Build());

            var cardServices = _mtgServiceProvider.GetCardService();
            Logger.LogInformation("Downloading first page");
            var firstPage = await cardServices.AllAsync();
            totalPages = firstPage.PagingInfo.TotalPages;
            firstPage.Value.ToMtgCards().ForEach(downloadedCards.Add);
            Logger.LogInformation("Found {TotalPages}", totalPages);
            await Enumerable.Range(2, totalPages).ParallelForEachAsync(async i =>
            {
                var cards = await DownloadMtgPage(tempPath, i, cardServices);
                Logger.LogInformation("Downloaded {CurrentPage}/{TotalPages}", i, totalPages);
                await PublishEvent(NotificationEventBuilder.Create().Broadcast().Title("Import MTG")
                    .Message("Importing cards").Progress(i, totalPages).Build());

                cards.ForEach(downloadedCards.Add);
            }, _numThreads);

            Logger.LogInformation("Starting import cards with {NumThreads}", _numImportThreads);
            await downloadedCards.ToList()
                .ParallelForEachAsync(async (card, l) => { await SaveMtgCard(card); }, _numImportThreads);
        }

        private async Task<List<MtgCard>> DownloadMtgPage(string tempPath, int page, ICardService cardService)
        {
            var cardFileName = Path.Join(tempPath, $"mtg_{page}.json");
            if (File.Exists(cardFileName))
            {
                return JsonConvert.DeserializeObject<List<MtgCard>>(await File.ReadAllTextAsync(cardFileName))!;
            }

            var pageResult = await cardService.Where(parameter => parameter.Page, page).AllAsync();
            var mtgCards = pageResult.Value.ToMtgCards();
            await File.WriteAllTextAsync(cardFileName, JsonConvert.SerializeObject(mtgCards));

            return mtgCards;
        }

        private async Task SaveMtgCard(MtgCard card)
        {
            var exists = await _mtgDumpDao.QueryAsSingle(entities =>
                entities.Where(s => s.CardName == card.Name && s.SetCode == card.Set));

            if (exists != null)
            {
                return;
            }

            exists = new MtgDumpEntity
            {
                Card = card,
                CardName = card.Name,
                ForeignNames = string.Join(',', card.ForeignNames.Select(s => s.Name)),
                ImageUrl = card.ImageUrl == null ? "" : card.ImageUrl.ToString(),
                MultiverseId = card.MultiverseId != null ? int.Parse(card.MultiverseId) : null,
                SetCode = card.Set
            };

            await _mtgDumpDao.Insert(exists);
            Logger.LogInformation("Inserted card {Name} [{SetCode}]", card.Name, card.Set);
        }
    }
}