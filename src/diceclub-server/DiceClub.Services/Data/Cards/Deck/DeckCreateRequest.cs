﻿namespace DiceClub.Services.Data.Cards.Deck;

public class DeckCreateRequest
{
    public string? Keyword { get; set; }
    public string DeckName { get; set; }
    public List<string> Colors { get; set; }
    public int TotalCards { get; set; }
    public int TotalSideBoard { get; set; }
    public List<DeckManaCurve>? ManaCurves { get; set; }

}