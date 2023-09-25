using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Chance;

public class ChanceDeck : BaseDeck<ChanceCard>
{
    public ChanceDeck() : this(new List<ChanceCard>())
    {
    }

    public ChanceDeck(IList<ChanceCard> deck) : base(deck)
    {
    }

    public static ChanceDeck DefaultDeck()
    {
        var deck = new ChanceDeck(DefaultChanceDeck.Deck);
        deck.Shuffle();
        return deck;
    }

    protected override void RegisterDraw(HorseInRace drawingHorse)
    {
        drawingHorse.ChanceCardsDrawn++;
    }
}