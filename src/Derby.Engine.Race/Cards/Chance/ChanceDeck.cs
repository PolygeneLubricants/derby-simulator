using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Chance;

/// <summary>
///     Specialized deck for Chance cards.
/// </summary>
public class ChanceDeck : BaseDeck<ChanceCard>
{
    public ChanceDeck() : this(new List<ChanceCard>())
    {
    }

    public ChanceDeck(IList<ChanceCard> deck) : base(deck)
    {
    }

    protected override void RegisterDraw(HorseInRace drawingHorse)
    {
        drawingHorse.ChanceCardsDrawn++;
    }
}