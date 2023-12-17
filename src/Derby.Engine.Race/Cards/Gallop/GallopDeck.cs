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

    /// <inheritdoc />
    protected override void RegisterDraw(HorseInRace drawingHorse)
    {
        drawingHorse.GallopCardsDrawn++;
    }
}