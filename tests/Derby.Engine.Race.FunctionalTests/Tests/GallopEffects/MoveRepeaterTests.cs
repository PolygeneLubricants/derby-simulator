using Derby.Engine.Race.Board.Lanes.Fields;
using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;
using Derby.Engine.Race.FunctionalTests.Utilities.TestModels;
using Derby.Engine.Race.Turns.Resolutions;

namespace Derby.Engine.Race.FunctionalTests.Tests.GallopEffects;

public class MoveRepeaterTests
{
    [Theory]
    [InlineData(2, 4)]
    [InlineData(3, 6)]
    public void MoveRepeaterEffect_WhenCardDrawn_RepeatsThisTurn(int initialMove, int finalLocation)
    {
        // Arrange
        var card1 = new GallopCard { Title = "", Description = "", CardEffect = new MoveRepeaterEffect() };
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(new List<IField>
            {
                new StartField(0),
                new NeutralField(10),
                new GallopField(100),
                new GallopField(110),
                new NeutralField(200),
                new NeutralField(300),
                new NeutralField(310),
                new NeutralField(320),
                new GoalField(400)
            }, 2)
            .WithHorseInRace(new[] { initialMove, 10 }, out _)
            .WithGallopCard(card1)
            .Build();

        // Act
        _ = race.ResolveTurn();

        // Assert
        Assert.Equal(finalLocation, race.State.RegisteredHorses.First().Location);
    }

    [Fact]
    public void MoveRepeaterEffect_WhenCardDrawnInHomeStretch_RepeatsLastTurn()
    {
        // Arrange
        var card1 = new ObservableGallopCard { Title = "", Description = "", CardEffect = new MoveRepeaterEffect() };
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(new List<IField>
            {
                new StartField(0),
                new NeutralField(10),
                new GallopField(100),
                new GoalField(300)
            }, 2)
            .WithHorseInRace(new[] { 1, 3 }, out _)
            .WithGallopCard(card1)
            .Build();

        // Act
        var drawnTimes = 0;
        card1.OnDraw += () => { drawnTimes++; };
        _ = race.ResolveTurn();
        var goalResolution = race.ResolveTurn();

        // Assert
        Assert.IsType<HorseWonTurnResolution>(goalResolution);
        Assert.Equal(1, drawnTimes);
    }

    [Fact]
    public void MoveRepeaterEffect_WhenLastMoveWasCardEffect_MovesLastNaturalMove()
    {
        // Arrange
        var repeaterCard   = new ObservableGallopCard { Title = "", Description = "", CardEffect = new MoveRepeaterEffect() };
        var moveCard       = new ObservableGallopCard { Title = "", Description = "", CardEffect = new MoveEffect(2) };
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(new List<IField>
            {
                new StartField(0),
                new GallopField(10),
                new NeutralField(100),
                new GallopField(110),
                new NeutralField(200),
                new GoalField(300)
            }, 2)
            .WithHorseInRace(new[] { 1, 1 }, out _)
            .WithGallopCard(moveCard)
            .WithGallopCard(repeaterCard)
            .Build();

        // Act
        _ = race.ResolveTurn();

        // Assert
        Assert.Equal(4, race.State.RegisteredHorses[0].Location);
    }
}