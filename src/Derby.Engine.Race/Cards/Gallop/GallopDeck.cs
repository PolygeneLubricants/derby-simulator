using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop;

/// <summary>
///     Specialized deck for Gallop cards.
/// </summary>
public class GallopDeck : BaseDeck<GallopCard>
{
    public GallopDeck() : this(new List<GallopCard>())
    {
    }

    public GallopDeck(IList<GallopCard> deck) : base(deck)
    {
    }

    /// <summary>
    ///     The pre-defined deck of Gallop cards in the Derby board game.
    /// </summary>
    /// <returns></returns>
    public static GallopDeck DefaultDeck()
    {
        var deck = new GallopDeck(DefaultGallopDeck.Deck);
        deck.Shuffle();
        return deck;
    }

    /// <inheritdoc />
    protected override void RegisterDraw(HorseInRace drawingHorse)
    {
        drawingHorse.GallopCardsDrawn++;
    }
}