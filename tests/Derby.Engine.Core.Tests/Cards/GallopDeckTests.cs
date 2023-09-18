using Derby.Engine.Models.Cards.Gallop;

namespace Derby.Engine.Core.Models.Tests.Cards;

public class GallopDeckTests
{
    [Fact]
    public void Ctor_WhenConfiguredCorrectly_InitsCorrectly()
    {
        // Arrange

        // Act
        var gallopDeck = new GallopDeck();

        // Assert
        Assert.NotNull(gallopDeck);
        Assert.NotEmpty(gallopDeck.Deck);
        Assert.Empty(gallopDeck.DiscardPile);
    }

    [Fact]
    public void Draw_WhenDeckHasCards_CardDrawn()
    {
        // Arrange
        var gallopDeck = new GallopDeck();
        var initialDeck = new List<GallopCard>(gallopDeck.Deck);

        // Act
        var cardDrawn = gallopDeck.Draw();

        // Assert
        Assert.Equal(1, gallopDeck.DiscardPile.Count);
        Assert.Equal(initialDeck.Count - 1, gallopDeck.Deck.Count);
        Assert.NotNull(cardDrawn);
    }

    [Fact]
    public void Draw_WhenDeckHasNoCards_DeckShuffledAndCardDrawn()
    {
        // Arrange
        var gallopDeck = new GallopDeck();
        var initialDeck = new List<GallopCard>(gallopDeck.Deck);
        for (var i = 0; i < initialDeck.Count; i++)
        {
            _ = gallopDeck.Draw();
        }

        // Act
        var cardDrawn = gallopDeck.Draw();
        

        // Assert
        Assert.Equal(1, gallopDeck.DiscardPile.Count);
        Assert.Equal(initialDeck.Count - 1, gallopDeck.Deck.Count);
        Assert.NotNull(cardDrawn);
    }
}