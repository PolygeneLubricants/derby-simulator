using Derby.Engine.Race.Board.Lanes.Fields;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;
using Derby.Engine.Race.FunctionalTests.Utilities.TestModels;
using Derby.Engine.Race.Turns.Resolutions;

namespace Derby.Engine.Race.FunctionalTests.Tests.GallopEffects;

public class MoveEffectTests
{
    [Fact]
    public void MoveEffect_WhenCardDrawn_Moves()
    {
        // Arrange
        var card1 = new GallopCard { Title = "", Description = "", CardEffect = new MoveEffect(1) };
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(new List<IField>
            {
                new StartField(0),
                new GallopField(100),
                new NeutralField(200),
                new GoalField(300)
            }, 2)
            .WithHorseInRace(new[] { 1 }, out _)
            .WithGallopCard(card1)
            .Build();

        // Act
        _ = race.ResolveTurn();

        // Assert
        Assert.Equal(2, race.State.RegisteredHorses.First().Location);
    }

    [Fact]
    public void MoveEffect_WhenCardDrawnNearGoal_MovesToGoal()
    {
        // Arrange
        var card1 = new ObservableGallopCard { Title = "", Description = "", CardEffect = new MoveEffect(1) };
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(new List<IField>
            {
                new StartField(0),
                new GallopField(100),
                new GoalField(300)
            }, 2)
            .WithHorseInRace(new[] { 1 }, out _)
            .WithGallopCard(card1)
            .Build();

        // Act
        var drawnTimes = 0;
        card1.OnDraw += () => { drawnTimes++; };
        var goalResolution = race.ResolveTurn();

        // Assert
        Assert.IsType<HorseWonTurnResolution>(goalResolution);
        Assert.Equal(1, drawnTimes);
    }

    [Fact]
    public void MoveEffect_WhenCardDrawnInHomeStretch_OnlyMovesOnEffectNotNatural()
    {
        // Arrange
        var card1 = new ObservableGallopCard { Title = "", Description = "", CardEffect = new MoveEffect(1) };
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(new List<IField>
            {
                new StartField(0),
                new NeutralField(100),
                new NeutralField(200),
                new GoalField(300)
            }, 2)
            .WithHorseInRace(new[] { 3, 1 }, out _)
            .WithGallopCard(card1)
            .Build();

        // Act
        var drawnTimes = 0;
        card1.OnDraw += () => { drawnTimes++; };
        _ = race.ResolveTurn(); // Home stretch, draw move card, move 1
        _ = race.ResolveTurn(); // Move 1

        // Assert
        Assert.Equal(1, drawnTimes);
        Assert.Equal(2, race.State.RegisteredHorses[0].Location);
    }
}