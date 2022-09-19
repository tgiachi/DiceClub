using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text.Json;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using Dasync.Collections;
using DiceClub.Api.Data.Cards.Mtg;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Entities.Cards;
using DiceClub.Services.Data.Card.Ml;
using Microsoft.Extensions.Logging;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Service;

namespace DiceClub.Services.Cards;

public class ImportMtgService : AbstractBaseService<ImportMtgService>
{
    private readonly ICardService _cardService;
    private readonly MtgDao _mtgDao;
    private readonly ImageMachineLearningService _imageMachineLearningService;

    public ImportMtgService(IEventBusService eventBusService, ILogger<ImportMtgService> logger,
        MtgServiceProvider mtgServiceProvider, MtgDao mtgDao, ImageMachineLearningService imageMachineLearningService) :
        base(eventBusService, logger)
    {
        _cardService = mtgServiceProvider.GetCardService();
        _mtgDao = mtgDao;
        _imageMachineLearningService = imageMachineLearningService;
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

    public async Task PrepareDataForMachineLearning()
    {
        var path = @"C:\TEMP\mtgml";
        var imagePath = Path.Join(path, "images");

        Directory.CreateDirectory(path);
        Directory.CreateDirectory(imagePath);
        var mlTsvData = new ConcurrentBag<CardMlTrainObject>();

        var cards = await _mtgDao.QueryAsList(s =>
            s.Where(k => !string.IsNullOrEmpty(k.ImageUrl) && k.MultiverseId != null));

        cards = cards.Take(3).ToList();

        await cards.ParallelForEachAsync(async (entity, l) =>
        {
            try
            {
                var fileName = await DownloadImage(imagePath, entity.ImageUrl, entity.CardName);
                Logger.LogInformation("Downloaded {Name} [{Idx}/{Total}]", entity.CardName, l, cards.Count);
                mlTsvData.Add(new CardMlTrainObject
                {
                    FileName = fileName,
                    MtgId = entity.MultiverseId.Value
                });
            }
            catch (Exception ex)
            {
                Logger.LogError("Error during import image: {Image} - {Ex}", entity.ImageUrl, ex);
            }
        }, 100);

        var outTsvList = new List<string>();
        foreach (var cardMlTrainObject in mlTsvData)
        {
            outTsvList.Add($"{cardMlTrainObject.FileName}\t{cardMlTrainObject.MtgId}");
        }

        await File.WriteAllLinesAsync(Path.Join(path, "mtg_labels.tsv"), outTsvList);

        await _imageMachineLearningService.TrainModel(Path.Join(Environment.CurrentDirectory, "Assets"), path,
            "mtg_labels.tsv");
    }

    private async Task<string> DownloadImage(string path, string url, string name)
    {
        using var httpClient = new HttpClient();

        var fileName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";

        var downloadFile = await httpClient.GetByteArrayAsync(url);
        var memoryStream = new MemoryStream(downloadFile);

        var imageToResize = Image.FromStream(memoryStream);
        var resisedImage = ResizeImage(imageToResize, new Size(233, 311));
        resisedImage.Save(Path.Join(path, fileName), ImageFormat.Jpeg);


        return fileName;
    }

    private static Image ResizeImage(Image imgToResize, Size size)
    {
        //Get the image current width  
        int sourceWidth = imgToResize.Width;
        //Get the image current height  
        int sourceHeight = imgToResize.Height;
        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;
        //Calulate  width with new desired size  
        nPercentW = ((float)size.Width / (float)sourceWidth);
        //Calculate height with new desired size  
        nPercentH = ((float)size.Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
            nPercent = nPercentH;
        else
            nPercent = nPercentW;
        //New Width  
        int destWidth = (int)(sourceWidth * nPercent);
        //New Height  
        int destHeight = (int)(sourceHeight * nPercent);
        Bitmap b = new Bitmap(destWidth, destHeight);
        Graphics g = Graphics.FromImage((System.Drawing.Image)b);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        // Draw image with new width and height  
        g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        g.Dispose();
        return (System.Drawing.Image)b;
    }

    private Task AddCards(List<MtgCard> cards)
    {
        return cards.ParallelForEachAsync(async card =>
        {
            var exists = await _mtgDao.FindByIdAndName(card.MultiverseId == null ? 0 : int.Parse(card.MultiverseId),
                card.Name);

            if (exists == null)
            {
                await _mtgDao.Insert(new MtgEntity
                {
                    MultiverseId = card.MultiverseId == null ? 0 : int.Parse(card.MultiverseId),
                    CardName = card.Name,
                    Card = card,
                    ForeignNames = string.Join(',', card.ForeignNames.Select(s => s.Name)),
                    ImageUrl = card.ImageUrl != null ? card.ImageUrl.ToString() : " ",
                    SetCode = card.Set
                });

                Logger.LogInformation("Adding {CardName}", card.Name);
            }
        }, 10);
    }
}