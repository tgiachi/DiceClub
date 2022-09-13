using MtgApiManager.Lib.Model;

namespace DiceClub.Api.Data.Cards.Mtg;

public static class CardToMtgCardConverter
{
    public static List<MtgCard> ToMtgCards(this List<ICard> cards)
    {
        return cards.Select(s => s.ToMtgCard()).ToList();
    }

    public static MtgCard ToMtgCard(this ICard card)
    {
        var c = new MtgCard
        {
            Artist = card.Artist,
            Border = card.Border,
            Flavor = card.Flavor,
            Cmc = card.Cmc,
            Colors = card.Colors,
            Hand = card.Hand,
            Id = card.Id,
            Layout = card.Layout,
            Life = card.Life,
            Loyalty = card.Loyalty,
            Name = card.Name,
            Names = card.Names,
            Number = card.Number,
            Power = card.Power,
            Printings = card.Printings,
            Rarity = card.Rarity,
            Reserved = card.Reserved,
            Set = card.Set,
            Source = card.Source,
            Starter = card.Starter,
            Text = card.Text,
            Timeshifted = card.Timeshifted,
            Toughness = card.Toughness,
            Type = card.Type,
            Types = card.Types,
            Variations = card.Variations,
            Watermark = card.Watermark,
            ColorIdentity = card.ColorIdentity,
            ImageUrl = card.ImageUrl,
            ManaCost = card.ManaCost,
            MultiverseId = card.MultiverseId,
            OriginalText = card.OriginalText,
            OriginalType = card.OriginalType,
            ReleaseDate = card.ReleaseDate,
            SetName = card.SetName,
            SubTypes = card.SubTypes,
            SuperTypes = card.SuperTypes,
            Legalities = new(),
            ForeignNames = new(),
            Rulings = new()
        };


        if (card.Legalities != null)
        {
            foreach (var l in card.Legalities)
            {
                c.Legalities.Add(new MtgLegality()
                {
                    Format = l.Format,
                    LegalityName = l.LegalityName
                });
            }
        }

        if (card.ForeignNames != null)
        {
            foreach (var l in card.ForeignNames)
            {
                c.ForeignNames.Add(new MtgForeignName()
                {
                    Language = l.Language,
                    Name = l.Name,
                    MultiverseId = l.MultiverseId
                });
            }
        }

        if (card.Rulings != null)
        {
            foreach (var l in card.Rulings)
            {
                c.Rulings.Add(new MtgRuling()
                {
                    Date = l.Date,
                    Text = l.Text
                });
            }
        }

        return c;
    }
}