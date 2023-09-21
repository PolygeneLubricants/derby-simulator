﻿namespace Derby.Engine.Race.Cards;

public class BaseDeck<T>
{
    public event Action<HorseInRace, T> CardDrawn = (horse, card) => { };

    public BaseDeck(IList<T> deck)
    {
        Deck = deck;
        DiscardPile = new List<T>();
    }

    public IList<T> Deck { get; internal set; }

    public IList<T> DiscardPile { get; internal set; }

    public T Draw(HorseInRace drawingHorse)
    {
        if (Deck.Count == 0)
        {
            Shuffle();
        }

        var drawnCard = Deck.First();
        Deck.RemoveAt(0);
        DiscardPile.Add(drawnCard);
        CardDrawn.Invoke(drawingHorse, drawnCard);
        return drawnCard;
    }

    protected void Shuffle()
    {
        var deck = new List<T>();
        deck.AddRange(Deck);
        deck.AddRange(DiscardPile);
        deck = deck.OrderBy(c => Guid.NewGuid()).ToList(); // Shuffle
        Deck = deck;
        DiscardPile = new List<T>();
    }
}