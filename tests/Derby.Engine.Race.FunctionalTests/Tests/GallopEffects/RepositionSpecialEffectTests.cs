using Derby.Engine.Race.Board.Lanes;
using Derby.Engine.Race.Board.Lanes.PredefinedLanes;
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
    [InlineData(3)] // On-par
    [InlineData(4)] // Overtakes with 1
    public void RepositionSpecialEffect_AsGallopCardWhenAwaiting_BehaveAsGallopCard(int overtakingMove)
    {
        // Arrange
        var card = new GallopCard { Title = "", Description = "", CardEffect = new HomeStretchCompositeEffect(new RepositionSpecialEffect(), new ModifierEffect(() => new AwaitModifier(AwaitType.Nearest))) };
        var builder = new RaceTestBuilder();
        var race =
            builder
                .WithPredefinedLanes()
                .WithHorseInRace(new[] { 1, 2, 2, 1 }, 4, out var _)
                .WithHorseInRace(new[] { 6, 1, 1, 1 }, 2, out var _)
                .WithHorseInRace(new[] { 3, overtakingMove, 1, 1 }, 3, out var _)
                .WithGallopCard(card)
                .WithNoEffectGallopCard()
                .Build();

        // Act
        _ = race.ResolveTurn(); // Move to 1
        _ = race.ResolveTurn(); // Move to 6 pick up Gallop and wait
        _ = race.ResolveTurn(); // Move to 3

        _ = race.ResolveTurn(); // Move to 3
        _ = race.ResolveTurn(); // Wait
        _ = race.ResolveTurn(); // Move to 6(7) (Overtakes/on-par with waiting horse)

        _ = race.ResolveTurn(); // Move to 5
        _ = race.ResolveTurn(); // Moves to 7
        _ = race.ResolveTurn(); // Move to 7(8)

        _ = race.ResolveTurn(); // Move to 6
        _ = race.ResolveTurn(); // Move to 8
        _ = race.ResolveTurn(); // Move to 8(9)

        // Assert
        Assert.Equal(6, race.State.RegisteredHorses[0].Location);
        Assert.Equal(8, race.State.RegisteredHorses[1].Location);
        Assert.Equal(5 + overtakingMove, race.State.RegisteredHorses[2].Location);
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
                .WithPredefinedLanes()
                .WithHorseInRace(new[] { 26, 1, 1, 0 }, 2, out var _)
                .WithHorseInRace(new[] { 28, 4, 1, 0 }, 3, out var _)
                .WithHorseInRace(new[] { 37, 3, 1, 1 }, 4, out var _)
                .WithGallopCard(card)
                .WithNoEffectChanceCard()
                .Build();

        // Act
        _ = race.ResolveTurn(); // 1 moves
        _ = race.ResolveTurn(); // 2 moves
        _ = race.ResolveTurn(); // 3 moves

        _ = race.ResolveTurn(); // 1 moves
        _ = race.ResolveTurn(); // 2 moves
        var skipsTurn = race.ResolveTurn(); // 3 ends turn and skips

        _ = race.ResolveTurn(); // 1 moves
        _ = race.ResolveTurn(); // 2 moves
        _ = race.ResolveTurn(); // 3 skips turn

        _ = race.ResolveTurn(); // 1 moves
        _ = race.ResolveTurn(); // 2 moves
        _ = race.ResolveTurn(); // 3 moves again

        // Assert
        Assert.Equal(28, race.State.RegisteredHorses[0].Location);
        Assert.Equal(33, race.State.RegisteredHorses[1].Location);
        Assert.Equal(38, race.State.RegisteredHorses[2].Location);
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
                .WithPredefinedLanes()
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
        Assert.Equal(27, race.State.RegisteredHorses[0].Location);
        Assert.Equal(32, race.State.RegisteredHorses[1].Location);
        Assert.Equal(40, race.State.RegisteredHorses[2].Location);
    }
}