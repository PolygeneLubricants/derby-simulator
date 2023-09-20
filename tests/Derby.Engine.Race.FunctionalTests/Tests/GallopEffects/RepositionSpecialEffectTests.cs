using Derby.Engine.Race.Board.Lanes;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;

namespace Derby.Engine.Race.FunctionalTests.Tests.GallopEffects;

// Afvent den nærmeste bagved liggende hest. I opløbet: Flyt i mål, hvis hesten ikke fører. Bliv ellers stående til næste omgang. Gem kortet til det ikke gælder længere.
public class RepositionSpecialEffectTests
{
    // Afvent den nærmeste bagved liggende hest.
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void RepositionSpecialEffect_AsGallopCardWhenAwaiting_BehaveAsGallopCard(int finalMove)
    {
        // Arrange
        var card = new GallopCard { Title = "", Description = "", CardEffect = new HomeStretchCompositeEffect(new RepositionSpecialEffect(), new ModifierEffect(() => new AwaitModifier(AwaitType.Nearest))) };
        var builder = new RaceTestBuilder();
        var race =
            builder
                .WithLane(new Lane2Years().Fields, 2)
                .WithLane(new Lane3Years().Fields, 3)
                .WithLane(new Lane4Years().Fields, 4)
                .WithHorseInRace(new[] { 6, 1, 1, 1 }, 2, out var _)
                .WithHorseInRace(new[] { 7, 4, 1, 1 }, 3, out var _)
                .WithHorseInRace(new[] { 1, 2, 2, finalMove }, 4, out var _)
                .WithGallopCard(card)
                .Build();

        // Act
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();

        _ = race.ResolveTurn();
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();

        _ = race.ResolveTurn();
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();

        _ = race.ResolveTurn();
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();

        var stoppedAwaiting = race.ResolveTurn();

        // Assert
        Assert.Equal(12, race.State.HorsesInRace[0].Location);
        Assert.Equal(13, race.State.HorsesInRace[1].Location);
        Assert.Equal(5 + finalMove, race.State.HorsesInRace[2].Location);
    }

    // I opløbet: Flyt i mål, hvis hesten ikke fører. Bliv ellers stående til næste omgang.
    [Fact]
    public void RepositionSpecialEffect_AsGallopCardWhenInHomeStretchAndLeading_Skip()
    {
        // Arrange
        var card = new GallopCard { Title = "", Description = "", CardEffect = new HomeStretchCompositeEffect(new RepositionSpecialEffect(), new ModifierEffect(() => new AwaitModifier(AwaitType.Nearest))) };
        var builder = new RaceTestBuilder();
        var race =
            builder
                .WithLane(new Lane2Years().Fields, 2)
                .WithLane(new Lane3Years().Fields, 3)
                .WithLane(new Lane4Years().Fields, 4)
                .WithHorseInRace(new[] { 26, 1 }, 2, out var _)
                .WithHorseInRace(new[] { 28, 4 }, 3, out var _)
                .WithHorseInRace(new[] { 37, 3 }, 4, out var _)
                .WithGallopCard(card)
                .WithNoEffectChanceCard()
                .Build();

        // Act
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();

        _ = race.ResolveTurn();
        _ = race.ResolveTurn();
        var skipsTurn = race.ResolveTurn();

        // Assert
        Assert.Equal(27, race.State.HorsesInRace[0].Location);
        Assert.Equal(32, race.State.HorsesInRace[1].Location);
        Assert.Equal(37, race.State.HorsesInRace[2].Location);
    }

    // I opløbet: Flyt i mål, hvis hesten ikke fører. Bliv ellers stående til næste omgang.
    [Fact]
    public void RepositionSpecialEffect_AsGallopCardWhenInHomeStretchAndNotLeading_Win()
    {
        // Arrange
        var card = new GallopCard { Title = "", Description = "", CardEffect = new HomeStretchCompositeEffect(new RepositionSpecialEffect(), new ModifierEffect(() => new AwaitModifier(AwaitType.Nearest))) };
        var builder = new RaceTestBuilder();
        var race =
            builder
                .WithLane(new Lane2Years().Fields, 2)
                .WithLane(new Lane3Years().Fields, 3)
                .WithLane(new Lane4Years().Fields, 4)
                .WithHorseInRace(new[] { 26, 1 }, 2, out var _)
                .WithHorseInRace(new[] { 28, 4 }, 3, out var _)
                .WithHorseInRace(new[] { 36, 4 }, 4, out var _)
                .WithGallopCard(card)
                .WithNoEffectChanceCard()
                .Build();

        // Act
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();

        _ = race.ResolveTurn();
        _ = race.ResolveTurn();
        var skipsTurn = race.ResolveTurn();

        // Assert
        Assert.Equal(27, race.State.HorsesInRace[0].Location);
        Assert.Equal(32, race.State.HorsesInRace[1].Location);
        Assert.Equal(40, race.State.HorsesInRace[2].Location);
    }
}