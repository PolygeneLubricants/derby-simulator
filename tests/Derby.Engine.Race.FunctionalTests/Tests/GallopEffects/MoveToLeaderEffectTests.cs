using Derby.Engine.Race.Board.Lanes;
using Derby.Engine.Race.Board.Lanes.Fields;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;
using Derby.Engine.Race.Turns.Resolutions;

namespace Derby.Engine.Race.FunctionalTests.Tests.GallopEffects;

public class MoveToLeaderEffectTests
{
    [Fact]
    public void MoveToLeader_WhenHorseIsLeader_DoesNotMove()
    {
        // Arrange
        var card1 = new GallopCard { Title = "", Description = "", CardEffect = new MoveToLeaderEffect(Position.OnPar) };
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
            .WithGallopCard(card1)
            .Build();

        // Act
        _ = race.ResolveTurn();

        // Assert
        Assert.Equal(1, race.State.RegisteredHorses.First().Location);
    }

    [Theory]
    [InlineData(1, typeof(EndTurnTurnResolution), 38)]
    [InlineData(6, typeof(HorseWonTurnResolution), 40)]
    public void MoveToLeader_AsGallopCard_BehaveAsGallopCard(int secondMovement, Type resolutionType, int expectedFinalLocation)
    {
        // Arrange
        var card = new GallopCard { Title = "", Description = "", CardEffect = new HomeStretchCompositeEffect(new MoveEffect(99), new MoveToLeaderEffect(Position.Behind)) };
        var builder = new RaceTestBuilder();
        var race =
            builder
                .WithLane(new Lane2Years().Fields, 2)
                .WithLane(new Lane3Years().Fields, 3)
                .WithLane(new Lane4Years().Fields, 4)
                .WithLane(new Lane5Years().Fields, 5)
                .WithHorseInRace(new[] { 30, 1 }, 2, out var _)
                .WithHorseInRace(new[] { 18, 2 }, 3, out var _)
                .WithHorseInRace(new[] { 34, secondMovement }, 4, out var _)
                .WithHorseInRace(new[] { 1, 2 }, 5, out var _)
                .WithGallopCard(card)
                .Build();

        // Act
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();

        _ = race.ResolveTurn();
        _ = race.ResolveTurn();
        var finalResolution = race.ResolveTurn();

        // Assert
        Assert.IsType(resolutionType, finalResolution);
        Assert.Equal(expectedFinalLocation, race.State.RegisteredHorses[2].Location);
    }

    [Theory]
    [MemberData(nameof(MoveToLeaderData))]
    public void MoveToLeader_WhenHorseIsLeader_MovesToLeader(
        int firstHorseMovement,
        int secondHorseMovement,
        int thirdHorseMovement,
        int fourthHorseMovement,
        Position position, 
        int expectedLocationAfterMove)
    {
        // Arrange
        var card1 = new GallopCard { Title = "", Description = "", CardEffect = new MoveToLeaderEffect(position) };
        var builder = new RaceTestBuilder();
        var race =
            builder
                .WithLane(new Lane2Years().Fields, 2)
                .WithLane(new Lane3Years().Fields, 3)
                .WithLane(new Lane4Years().Fields, 4)
                .WithLane(new Lane5Years().Fields, 5)
                .WithHorseInRace(new[] { firstHorseMovement }, 2, out var _)
                .WithHorseInRace(new[] { secondHorseMovement }, 3, out var _)
                .WithHorseInRace(new[] { thirdHorseMovement }, 4, out var _)
                .WithHorseInRace(new[] { fourthHorseMovement }, 5, out var _)
                .WithGallopCard(card1)
                .WithNoEffectChanceCard()
                .Build();

        // Act
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();

        // Assert
        Assert.Equal(expectedLocationAfterMove, race.State.RegisteredHorses[3].Location);
    }

    public static IEnumerable<object[]> MoveToLeaderData =>
        new List<object[]>
        {
            new object[] { 24, 27, 31, 2, Position.Behind, 35 },
            new object[] { 24, 27, 31, 2, Position.OnPar, 36 },

            new object[] { 5, 4, 3, 2, Position.OnPar, 5 },
            new object[] { 5, 4, 3, 2, Position.Behind, 4 },

            new object[] { 5, 4, 39, 2, Position.OnPar, 43 },
            new object[] { 5, 4, 39, 2, Position.Behind, 42 },

            new object[] { 10, 1, 1, 2, Position.OnPar, 15 },
            new object[] { 10, 1, 1, 2, Position.Behind, 14 },
        };
}