using Derby.Engine.Race.Board.Lanes;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;
using Derby.Engine.Race.Turns.Resolutions;

namespace Derby.Engine.Race.FunctionalTests.Tests;

public class MoveTests
{
    [Fact]
    public void Move_WhenOneHorseMoves_MovesOk()
    {
        // Arrange
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(3)
            .WithHorseInRace(new[] { 1 }, out _)
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

    [Fact]
    public void Move_WhenOneHorseMovesToGoal_MovesToGoalOk()
    {
        // Arrange
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(3)
            .WithHorseInRace(new[] { 1 }, out _)
            .WithNoEffectGallopCard()
            .Build();

        // Act
        _ = race.ResolveTurn();
        var resolutionTurn2 = race.ResolveTurn();

        // Assert
        Assert.IsType<HorseWonTurnResolution>(resolutionTurn2);
        Assert.Equal(2, race.State.HorsesInRace.First().Location);
        Assert.Equal(0, race.State.HorsesInRace.First().FieldsFromGoal);
        Assert.Equal(2, race.State.CurrentTurn);
        Assert.Equal(0, race.State.NextHorseInTurn);

        var horseWonTurnResolution = resolutionTurn2 as HorseWonTurnResolution;
        Assert.Equal(1, horseWonTurnResolution.Score.Count);
        Assert.Equal(race.State.HorsesInRace.First(), horseWonTurnResolution.Score.First());
    }

    [Fact]
    public void Move_WhenOneHorseMovesToGoalButOvershoots_MovesToGoalOk()
    {
        // Arrange
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(3)
            .WithHorseInRace(new[] { 100 }, out _)
            .WithNoEffectGallopCard()
            .Build();

        // Act
        var resolution = race.ResolveTurn();

        // Assert
        Assert.IsType<HorseWonTurnResolution>(resolution);
        Assert.Equal(2, race.State.HorsesInRace.First().Location);
        Assert.Equal(0, race.State.HorsesInRace.First().FieldsFromGoal);
        Assert.Equal(1, race.State.CurrentTurn);
        Assert.Equal(0, race.State.NextHorseInTurn);

        var horseWonTurnResolution = resolution as HorseWonTurnResolution;
        Assert.Equal(1, horseWonTurnResolution.Score.Count);
        Assert.Equal(race.State.HorsesInRace.First(), horseWonTurnResolution.Score.First());
    }

    [Fact]
    public void Move_WhenFiveHorsesMoveOnce_HorsesMoveOk()
    {
        // Arrange
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(10)
            .WithHorseInRace(new[] { 1 }, out _)
            .WithHorseInRace(new[] { 2 }, out _)
            .WithHorseInRace(new[] { 3 }, out _)
            .WithHorseInRace(new[] { 4 }, out _)
            .WithHorseInRace(new[] { 5 }, out _)
            .WithNoEffectGallopCard()
            .Build();

        // Act
        var resolutions = Enumerable.Range(0, race.State.HorsesInRace.Count).Select(_ => race.ResolveTurn()).ToList();

        // Assert
        Assert.All(resolutions, resolution => Assert.IsType<EndTurnTurnResolution>(resolution));

        Assert.Equal(1, race.State.HorsesInRace[0].Location);
        Assert.Equal(8, race.State.HorsesInRace[0].FieldsFromGoal);
        Assert.Equal(2, race.State.HorsesInRace[1].Location);
        Assert.Equal(7, race.State.HorsesInRace[1].FieldsFromGoal);
        Assert.Equal(3, race.State.HorsesInRace[2].Location);
        Assert.Equal(6, race.State.HorsesInRace[2].FieldsFromGoal);
        Assert.Equal(4, race.State.HorsesInRace[3].Location);
        Assert.Equal(5, race.State.HorsesInRace[3].FieldsFromGoal);
        Assert.Equal(5, race.State.HorsesInRace[4].Location);
        Assert.Equal(4, race.State.HorsesInRace[4].FieldsFromGoal);

        Assert.Equal(1, race.State.CurrentTurn);
        Assert.Equal(0, race.State.NextHorseInTurn);
    }

    [Fact]
    public void Move_WhenFiveHorsesMoveUntilGoal_FastestMovesToGoalOk()
    {
        // Arrange
        var iterationTimeout = 10;
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(10)
            .WithHorseInRace(new[] { 1 }, out _)
            .WithHorseInRace(new[] { 2 }, out _)
            .WithHorseInRace(new[] { 3 }, out _)
            .WithHorseInRace(new[] { 5 }, out var winnerHorse)
            .WithHorseInRace(new[] { 4 }, out _)
            .WithNoEffectGallopCard()
            .Build();

        // Act
        ITurnResolution resolution = null;
        var iteration = 0;
        while (resolution is not HorseWonTurnResolution && iteration < iterationTimeout)
        {
            resolution = race.ResolveTurn();

            iteration++;
        }

        // Assert
        Assert.IsType<HorseWonTurnResolution>(resolution);
        var horseWonResolution = resolution as HorseWonTurnResolution;
        Assert.Equal(winnerHorse, horseWonResolution.Score.First().OwnedHorse);
        Assert.Equal(2, race.State.HorsesInRace[0].Location);
        Assert.Equal(4, race.State.HorsesInRace[1].Location);
        Assert.Equal(6, race.State.HorsesInRace[2].Location);
        Assert.Equal(9, race.State.HorsesInRace[3].Location);
        Assert.Equal(4, race.State.HorsesInRace[4].Location);

        Assert.Equal(1, race.State.CurrentTurn);
        Assert.Equal(4, race.State.NextHorseInTurn);
    }

    [Fact]
    public void Move_WhenTwoHorsesMoveUntilGoalButClose_FastestMovesToGoalOk()
    {
        // Arrange
        var iterationTimeout = 20;
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(10)
            .WithHorseInRace(new[] { 1 }, out var winnerHorse)
            .WithHorseInRace(new[] { 1 }, out _)
            .WithNoEffectGallopCard()
            .Build();

        // Act
        ITurnResolution resolution = null;
        var iteration = 0;
        while (resolution is not HorseWonTurnResolution && iteration < iterationTimeout)
        {
            resolution = race.ResolveTurn();

            iteration++;
        }

        // Assert
        Assert.IsType<HorseWonTurnResolution>(resolution);
        var horseWonResolution = resolution as HorseWonTurnResolution;
        Assert.Equal(winnerHorse, horseWonResolution.Score.First().OwnedHorse);
        Assert.Equal(9, race.State.HorsesInRace[0].Location);
        Assert.Equal(8, race.State.HorsesInRace[1].Location);

        Assert.Equal(8, race.State.CurrentTurn);
        Assert.Equal(1, race.State.NextHorseInTurn);
    }

    [Fact]
    public void Move_WhenThreeHorsesWithComplexMovePatternsRace_FastestWin()
    {
        // Arrange
        var iterationTimeout = 50;
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(33)
            .WithHorseInRace(new[] { 1, 2, 3, 3, 4, 2, 4, 3, 1, 3, 2, 4 }, out var winnerHorse)
            .WithHorseInRace(new[] { 4, 3, 2, 2, 2, 3, 2, 4, 2, 3, 1, 4 }, out _)
            .WithHorseInRace(new[] { 4, 3, 2, 3, 2, 3, 4, 1, 4, 3, 2, 1 }, out _)
            .WithNoEffectGallopCard()
            .Build();

        // Act
        ITurnResolution resolution = null;
        var iteration = 0;
        while (resolution is not HorseWonTurnResolution && iteration < iterationTimeout)
        {
            resolution = race.ResolveTurn();

            iteration++;
        }

        // Assert
        Assert.IsType<HorseWonTurnResolution>(resolution);
        var horseWonResolution = resolution as HorseWonTurnResolution;
        Assert.Equal(winnerHorse, horseWonResolution.Score.First().OwnedHorse);
        Assert.Equal(32, race.State.HorsesInRace[0].Location);
        Assert.Equal(28, race.State.HorsesInRace[1].Location);
        Assert.Equal(31, race.State.HorsesInRace[2].Location);

        Assert.Equal(11, race.State.CurrentTurn);
        Assert.Equal(1, race.State.NextHorseInTurn);
    }

    [Fact]
    public void Move_WhenTwoHorsesAreInSeparateLaneWithCurvature_CorrectlyDeterminesLastHorse()
    {
        // Arrange
        var builder = new RaceTestBuilder();
        var race =
            builder
                .WithLane(new Lane2Years().Fields, 2)
                .WithLane(new Lane3Years().Fields, 3)
                .WithHorseInRace(new[] { 12 }, 2, out _)
                .WithHorseInRace(new[] { 13 }, 3, out var lastHorse)
                .WithNoEffectGallopCard()
                .Build();

        // Act
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();

        // Assert
        Assert.Equal(lastHorse, race.State.GetLastHorse().OwnedHorse);
        Assert.Equal(12, race.State.HorsesInRace[0].Location);
        Assert.Equal(13, race.State.HorsesInRace[1].Location);
    }

    [Fact]
    public void Move_WhenTwoHorsesAreInSeparateLaneAfterCurvature_CorrectlyDeterminesLeader()
    {
        // Arrange
        var builder = new RaceTestBuilder();
        var race =
            builder
                .WithLane(new Lane2Years().Fields, 2)
                .WithLane(new Lane3Years().Fields, 3)
                .WithHorseInRace(new[] { 13 }, 2, out var leader)
                .WithHorseInRace(new[] { 15 }, 3, out _)
                .WithNoEffectGallopCard()
                .Build();

        // Act
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();

        // Assert
        Assert.Equal(leader, race.State.GetLeaderHorse().OwnedHorse);
        Assert.Equal(13, race.State.HorsesInRace[0].Location);
        Assert.Equal(15, race.State.HorsesInRace[1].Location);
    }

    [Fact]
    public void Move_WhenFourHorsesAreInFirstCurve_CorrectlyDeterminesLeaderAndLoser()
    {
        // Arrange
        var builder = new RaceTestBuilder();
        var race =
            builder
                .WithLane(new Lane2Years().Fields, 2)
                .WithLane(new Lane3Years().Fields, 3)
                .WithLane(new Lane4Years().Fields, 4)
                .WithLane(new Lane5Years().Fields, 5)
                .WithHorseInRace(new[] { 9 }, 2, out var first)
                .WithHorseInRace(new[] { 10 }, 3, out var second)
                .WithHorseInRace(new[] { 11 }, 4, out var third)
                .WithHorseInRace(new[] { 12 }, 5, out var fourth)
                .WithNoEffectGallopCard()
                .WithNoEffectChanceCard()
                .Build();

        // Act
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();

        // Assert
        Assert.Equal(first, race.State.GetLeaderHorse().OwnedHorse);
        Assert.Equal(fourth, race.State.GetLastHorse().OwnedHorse);

        Assert.Equal(first, race.State.GetScore()[0].OwnedHorse);
        Assert.Equal(second, race.State.GetScore()[1].OwnedHorse);
        Assert.Equal(third, race.State.GetScore()[2].OwnedHorse);
        Assert.Equal(fourth, race.State.GetScore()[3].OwnedHorse);
    }

    [Fact]
    public void Move_WhenFourHorsesAreInSecondCurve_CorrectlyDeterminesLeaderAndLoser()
    {
        // Arrange
        var builder = new RaceTestBuilder();
        var race =
            builder
                .WithLane(new Lane2Years().Fields, 2)
                .WithLane(new Lane3Years().Fields, 3)
                .WithLane(new Lane4Years().Fields, 4)
                .WithLane(new Lane5Years().Fields, 5)
                .WithHorseInRace(new[] { 24 }, 2, out var first)
                .WithHorseInRace(new[] { 27 }, 3, out var fourth)
                .WithHorseInRace(new[] { 31 }, 4, out var third)
                .WithHorseInRace(new[] { 35 }, 5, out var second)
                .WithNoEffectGallopCard()
                .Build();

        // Act
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();

        // Assert
        Assert.Equal(first, race.State.GetLeaderHorse().OwnedHorse);
        Assert.Equal(fourth, race.State.GetLastHorse().OwnedHorse);

        Assert.Equal(first, race.State.GetScore()[0].OwnedHorse);
        Assert.Equal(second, race.State.GetScore()[1].OwnedHorse);
        Assert.Equal(third, race.State.GetScore()[2].OwnedHorse);
        Assert.Equal(fourth, race.State.GetScore()[3].OwnedHorse);
    }

    [Fact]
    public void Move_WhenTwoHorsesAreOnSameField_CorrectlyDeterminesLeader()
    {
        // Arrange
        var builder = new RaceTestBuilder();
        var race =
            builder
                .WithLane(new Lane2Years().Fields, 2)
                .WithHorseInRace(new[] { 12 }, 2, out var leader)
                .WithHorseInRace(new[] { 12 }, 2, out _)
                .WithNoEffectGallopCard()
                .Build();

        // Act
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();

        // Assert
        Assert.Equal(leader, race.State.GetLeaderHorse().OwnedHorse);
        Assert.Equal(12, race.State.HorsesInRace[0].Location);
        Assert.Equal(12, race.State.HorsesInRace[1].Location);
    }
}