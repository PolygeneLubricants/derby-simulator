using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;
using Derby.Engine.Race.Turns.Resolutions;

namespace Derby.Engine.Race.FunctionalTests;

public class MoveTests
{
    [Fact]
    public void Move_WhenOneHorseMoves_MovesOk()
    {
        // Arrange
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(3)
            .WithHorseInRace(new[] { 1 })
            .Build();

        // Act
        var resolution = race.ResolveTurn();

        // Assert
        Assert.IsType<EndTurnTurnResolution>(resolution);
        Assert.Equal(1, race.State.HorsesInRace.First().Location);
        Assert.Equal(1, race.State.HorsesInRace.First().FieldsFromGoal);
        Assert.Equal(1, race.State.CurrentTurn);
        Assert.Equal(0, race.State.NextHorseInTurn);
    }
}