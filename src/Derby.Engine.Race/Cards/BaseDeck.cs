using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards;

/// <summary>
///     Base class for all decks of cards.
/// </summary>
/// <typeparam name="T">Card-type which this deck contains.</typeparam>
public abstract class BaseDeck<T>
{
    protected BaseDeck(IList<T> deck)
    {
        Deck = deck;
        DiscardPile = new List<T>();
    }

    /// <summary>
    ///     The current deck from which cards can be drawn.
    ///     When the deck is shuffled, the <see cref="DiscardPile" /> will be added to this.
    /// </summary>
    public IList<T> Deck { get; internal set; }

    /// <summary>
    ///     The currently drawn cards of the deck.
    ///     When the deck is shuffled, this will be emptied.
    /// </summary>
    public IList<T> DiscardPile { get; internal set; }

    /// <summary>
    ///     Event listener which fires when a card is drawn.
    /// </summary>
    public event Action<HorseInRace, T> CardDrawn = (horse, card) => { };

    /// <summary>
    ///     Draw a card on behalf of <paramref name="drawingHorse" />.
    /// </summary>
    public T Draw(HorseInRace drawingHorse)
    {
        RegisterDraw(drawingHorse);

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

    /// <summary>
    ///     Pre-call to drawing a card which enables implementing classes to applying functionality on drawing a card.
    /// </summary>
    /// <param name="drawingHorse">The horse to draw a card.</param>
    protected abstract void RegisterDraw(HorseInRace drawingHorse);

    /// <summary>
    ///     Shuffle the discard pile into the deck.
    /// </summary>
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