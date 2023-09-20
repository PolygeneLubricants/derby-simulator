using Derby.Engine.Race.Board.Lanes.Fields;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;

namespace Derby.Engine.Race.FunctionalTests.Tests.GallopEffects;

public class EndTurnAndSkipEffectTests
{
    [Fact]
    public void EndTurnAndSkip_WhenCardDrawn_TurnSkipped()
    {
        // Arrange
        var card = new GallopCard { Title = "", Description = "", CardEffect = new EndTurnAndSkipEffect() };
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(new List<IField>
            {
                new StartField(0), 
                new GallopField(100),
                new NeutralField(200),
                new GoalField(300)
            }, 2)
            .WithHorseInRace(new[] { 1 }, out _)
            .WithHorseInRace(new[] { 2 }, out _)
            .WithGallopCard(card)
            .WithNoEffectGallopCard()
            .Build();

        // Act
        _ = race.ResolveTurn(); // draws skip
        _ = race.ResolveTurn(); // moves
        _ = race.ResolveTurn(); // skips turn
        _ = race.ResolveTurn(); // horse 2 wins

        // Assert
        Assert.Equal(1, race.State.HorsesInRace[0].Location);
        Assert.Equal(3, race.State.HorsesInRace[1].Location);
    }

    [Fact]
    public void EndTurnAndSkip_WhenCardDrawn_TurnSkippedAndThenContinues()
    {
        // Arrange
        var card = new GallopCard { Title = "", Description = "", CardEffect = new EndTurnAndSkipEffect() };
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(new List<IField>
            {
                new StartField(0),
                new GallopField(100),
                new NeutralField(200),
                new GoalField(300)
            }, 2)
            .WithHorseInRace(new[] { 1 }, out _)
            .WithHorseInRace(new[] { 1 }, out _)
            .WithGallopCard(card)
            .WithNoEffectGallopCard()
            .Build();

        // Act
        _ = race.ResolveTurn(); // 1 draws skip
        _ = race.ResolveTurn(); // 2 moves
        _ = race.ResolveTurn(); // 1 skips turn
        _ = race.ResolveTurn(); // 2 moves
        _ = race.ResolveTurn(); // 1 skip over, moves

        // Assert
        Assert.Equal(2, race.State.HorsesInRace[0].Location);
        Assert.Equal(2, race.State.HorsesInRace[1].Location);
    }
}