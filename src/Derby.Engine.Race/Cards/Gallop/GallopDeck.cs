using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

namespace Derby.Engine.Race.Cards.Gallop;

public class GallopDeck : BaseDeck<GallopCard>
{
    public GallopDeck() : this(new List<GallopCard>())
    {
    }

    public GallopDeck(IList<GallopCard> deck) : base(deck)
    {
    }

    public static GallopDeck DefaultDeck()
    {
        var deck = new GallopDeck(DefaultGallopDeck.Deck);
        deck.Shuffle();
        return deck;
    }

    protected override void RegisterDraw(HorseInRace drawingHorse)
    {
        drawingHorse.GallopCardsDrawn++;
    }
}