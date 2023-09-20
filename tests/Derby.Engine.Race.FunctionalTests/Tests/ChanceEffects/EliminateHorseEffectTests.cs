using Derby.Engine.Race.Board.Lanes.Fields;
using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Chance.Effects;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;
using Derby.Engine.Race.Turns.Resolutions;

namespace Derby.Engine.Race.FunctionalTests.Tests.ChanceEffects;

public class EliminateHorseEffectTests
{
    [Fact]
    public void EliminateHorseEffect_WhenCardDrawnAndRaceHasMultipleHorses_HorseEliminatedAndRaceContinues()
    {
        // Arrange
        var card = new ChanceCard { Title = "", Description = "", CardEffect = new EliminateHorseEffect() };
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(new List<IField>
            {
                new StartField(0),
                new ChanceField(100),
                new NeutralField(200),
                new GoalField(300)
            }, 2)
            .WithHorseInRace(new[] { 1 }, out var eliminatedHorse)
            .WithHorseInRace(new[] { 2 }, out _)
            .WithChanceCard(card)
            .Build();

        // Act
        var eliminationResolution = race.ResolveTurn();
        _ = race.ResolveTurn();

        // Assert
        Assert.IsType<HorseEliminatedTurnResolution>(eliminationResolution);
        var eliminatedTurnResolution = eliminationResolution as HorseEliminatedTurnResolution;
        Assert.Equal(eliminatedHorse, eliminatedTurnResolution.EliminatedHorse.OwnedHorse);
        Assert.Equal(2, race.State.HorsesInRace[1].Location);
    }

    [Fact]
    public void EliminateHorseEffect_WhenCardDrawnAndRaceHaOneHorse_HorseEliminatedAndGameDraw()
    {
        // Arrange
        var card = new ChanceCard { Title = "", Description = "", CardEffect = new EliminateHorseEffect() };
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(new List<IField>
            {
                new StartField(0),
                new ChanceField(100),
                new NeutralField(200),
                new GoalField(300)
            }, 2)
            .WithHorseInRace(new[] { 1 }, out var eliminatedHorse)
            .WithChanceCard(card)
            .Build();

        // Act
        var eliminationResolution = race.ResolveTurn();
        var drawResolution = race.ResolveTurn();

        // Assert
        Assert.IsType<HorseEliminatedTurnResolution>(eliminationResolution);
        var eliminatedTurnResolution = eliminationResolution as HorseEliminatedTurnResolution;
        Assert.Equal(eliminatedHorse, eliminatedTurnResolution.EliminatedHorse.OwnedHorse);

        Assert.IsType<DrawTurnResolution>(drawResolution);
    }
}