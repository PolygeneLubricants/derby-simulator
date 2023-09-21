using Derby.Engine.Race.Cards.Gallop;

namespace Derby.Engine.Race.Tests.Cards;

public class GallopDeckTests
{
    [Fact]
    public void Ctor_WhenConfiguredCorrectly_InitsCorrectly()
    {
        // Arrange

        // Act
        var gallopDeck = GallopDeck.DefaultDeck();

        // Assert
        Assert.NotNull(gallopDeck);
        Assert.NotEmpty(gallopDeck.Deck);
        Assert.Empty(gallopDeck.DiscardPile);
    }

    [Fact]
    public void Draw_WhenDeckHasCards_CardDrawn()
    {
        // Arrange
        var gallopDeck = GallopDeck.DefaultDeck();
        var initialDeck = new List<GallopCard>(gallopDeck.Deck);

        // Act
        var cardDrawn = gallopDeck.Draw(null);

        // Assert
        Assert.Equal(1, gallopDeck.DiscardPile.Count);
        Assert.Equal(initialDeck.Count - 1, gallopDeck.Deck.Count);
        Assert.NotNull(cardDrawn);
    }

    [Fact]
    public void Draw_WhenDeckHasNoCards_DeckShuffledAndCardDrawn()
    {
        // Arrange
        var gallopDeck = GallopDeck.DefaultDeck();
        var initialDeck = new List<GallopCard>(gallopDeck.Deck);
        for (var i = 0; i < initialDeck.Count; i++)
        {
            _ = gallopDeck.Draw(null);
        }

        // Act
        var cardDrawn = gallopDeck.Draw(null);
        

        // Assert
        Assert.Equal(1, gallopDeck.DiscardPile.Count);
        Assert.Equal(initialDeck.Count - 1, gallopDeck.Deck.Count);
        Assert.NotNull(cardDrawn);
    }
}