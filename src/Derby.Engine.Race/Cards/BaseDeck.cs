namespace Derby.Engine.Race.Cards;

public class BaseDeck<T>
{
    private event Action<T>? _cardDrawn;

    public BaseDeck(IList<T> deck, Action<T>? cardDrawn)
    {
        _cardDrawn = cardDrawn;
        Deck = deck;
        DiscardPile = new List<T>();
    }

    public IList<T> Deck { get; internal set; }

    public IList<T> DiscardPile { get; internal set; }

    public T Draw()
    {
        if (Deck.Count == 0)
        {
            Shuffle();
        }

        var drawnCard = Deck.First();
        Deck.RemoveAt(0);
        DiscardPile.Add(drawnCard);

        if (_cardDrawn != null)
        {
            _cardDrawn.Invoke(drawnCard);
        }

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