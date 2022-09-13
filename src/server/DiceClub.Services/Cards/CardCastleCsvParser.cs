using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using DiceClub.Api.Data.Cards;

namespace DiceClub.Services.Cards
{
    public class CardCastleCsvParser
    {
        public List<CardCastleRecord> ParseCsv(string fileName)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };
            using var reader = new StreamReader(fileName);
            using var csv = new CsvReader(reader, config);
            return csv.GetRecords<CardCastleRecord>().ToList();
        }
    }
}
