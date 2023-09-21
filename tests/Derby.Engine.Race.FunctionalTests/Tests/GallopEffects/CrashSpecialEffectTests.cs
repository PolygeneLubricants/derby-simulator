using Derby.Engine.Race.Board.Lanes;
using Derby.Engine.Race.Board.Lanes.Fields;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;
using Derby.Engine.Race.Turns.Resolutions;

namespace Derby.Engine.Race.FunctionalTests.Tests.GallopEffects;

public class CrashSpecialEffectTests
{
    [Fact]
    public void CrashSpecialEffect_WhenCardDrawnInSameLane_WaitForLastToPass()
    {
        // Arrange
        var card1 = new GallopCard{ Title = "", Description = "", CardEffect = new CrashSpecialEffect() };
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(new List<IField>
            {
                new StartField(0), 
                new NeutralField(100), 
                new GallopField(200), 
                new NeutralField(300), 
                new NeutralField(400), 
                new GoalField(500)
            }, 2)
            .WithHorseInRace(new[] { 2 }, out _)
            .WithHorseInRace(new[] { 2 }, out _)
            .WithHorseInRace(new[] { 1 }, out _)
            .WithGallopCard(card1)
            .WithNoEffectGallopCard()
            .WithNoEffectGallopCard()
            .Build();

        // Act
        var drawsCrashResolution = race.ResolveTurn();
        var horseOnParResolution = race.ResolveTurn();
        var horseBehindResolution = race.ResolveTurn();
        var turnSkippedResolution = race.ResolveTurn();
        var passingHorseAdvancedFurtherResolution = race.ResolveTurn();
        var horseBehindNowOnParResolution = race.ResolveTurn();
        var crashedHorseMoves = race.ResolveTurn();

        // Assert
        Assert.Equal(4, race.State.RegisteredHorses[0].Location);
        Assert.Equal(4, race.State.RegisteredHorses[1].Location);
        Assert.Equal(2, race.State.RegisteredHorses[2].Location);
        Assert.Equal(2, race.State.CurrentTurn);
        Assert.Equal(1, race.State.NextHorseInTurn);
    }

    [Fact]
    public void CrashSpecialEffect_WhenCardDrawnInOtherLane_WaitForLastToPass()
    {
        // Arrange
        var card1 = new GallopCard { Title = "", Description = "", CardEffect = new CrashSpecialEffect() };
        var builder = new RaceTestBuilder();
        var race = builder
            .WithLane(new Lane2Years().Fields, 2)
            .WithLane(new Lane3Years().Fields, 3)
            .WithLane(new Lane4Years().Fields, 4)
            .WithLane(new Lane5Years().Fields, 5)
            .WithHorseInRace(new[] { 3 }, 2, out var _)
            .WithHorseInRace(new[] { 6 }, 3, out var _) // Draws crash
            .WithHorseInRace(new[] { 1 }, 4, out var _)
            .WithHorseInRace(new[] { 1 }, 5, out var _)
            .WithNoEffectChanceCard()
            .WithGallopCard(card1)
            .WithNoEffectGallopCard()
            .WithNoEffectGallopCard()
            .WithNoEffectGallopCard()
            .WithNoEffectGallopCard()
            .WithNoEffectGallopCard()
            .WithNoEffectGallopCard()
            .Build();

        // Act
        var movesAhead1 = race.ResolveTurn(); // 3
        var drawsCrash = race.ResolveTurn(); // 6
        var movesBehind1 = race.ResolveTurn(); // 1
        var movesBehind2 = race.ResolveTurn(); // 1

        var drawsNoEffect = race.ResolveTurn(); // 6
        var skips1 = race.ResolveTurn(); // 6
        var movesBehind3AndDraws = race.ResolveTurn(); // 2
        var movesBehind4AndDraws = race.ResolveTurn(); // 2

        var drawsChance = race.ResolveTurn(); // 9
        var skips2 = race.ResolveTurn(); // 6
        var movesBehind5 = race.ResolveTurn(); // 3
        var movesBehind6 = race.ResolveTurn(); // 3

        var drawsGallop = race.ResolveTurn(); // 12
        var skips3 = race.ResolveTurn(); // 6
        var movesBehind7DrawChance = race.ResolveTurn(); // 4
        var movesBehind8DrawChance = race.ResolveTurn(); // 4

        var movesAhead2 = race.ResolveTurn(); // 15
        var skips4 = race.ResolveTurn(); // 6
        var movesBehind9DrawChance = race.ResolveTurn(); // 5
        var movesBehind10DrawChance = race.ResolveTurn(); // 5

        var movesAhead3 = race.ResolveTurn(); // 18
        var skips5 = race.ResolveTurn(); // 6
        var onPar1 = race.ResolveTurn(); // 6
        var onPar2 = race.ResolveTurn(); // 6

        var movesAhead4 = race.ResolveTurn(); // 21
        var moves = race.ResolveTurn(); // 12
        var behind1 = race.ResolveTurn(); // 7
        var behind2 = race.ResolveTurn(); // 7

        // Assert
        Assert.Equal(21, race.State.RegisteredHorses[0].Location);
        Assert.Equal(12, race.State.RegisteredHorses[1].Location);
        Assert.Equal(7, race.State.RegisteredHorses[2].Location);
        Assert.Equal(7, race.State.RegisteredHorses[3].Location);
        Assert.Equal(7, race.State.CurrentTurn);
        Assert.Equal(0, race.State.NextHorseInTurn);
    }

    [Fact]
    public void CrashSpecialEffect_WhenCardDrawnAndOtherHorseWins_HorseThatDrewIsLast()
    {
        // Arrange
        var card1 = new GallopCard { Title = "", Description = "", CardEffect = new CrashSpecialEffect() };
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(new List<IField>
            {
                new StartField(0),
                new NeutralField(100),
                new GallopField(200),
                new NeutralField(300),
                new NeutralField(400),
                new GoalField(500)
            }, 2)
            .WithHorseInRace(new[] { 2 }, out var lastHorse)
            .WithHorseInRace(new[] { 1 }, out _)
            .WithHorseInRace(new[] { 5 }, out _)
            .WithGallopCard(card1)
            .WithNoEffectGallopCard()
            .WithNoEffectGallopCard()
            .Build();

        // Act
        var drawsCrashResolution = race.ResolveTurn();
        var horseAdvancedResolution = race.ResolveTurn();
        var horseInGoalResolution = race.ResolveTurn();

        // Assert
        Assert.IsType<HorseWonTurnResolution>(horseInGoalResolution);
        var wonResolution = horseInGoalResolution as HorseWonTurnResolution;
        Assert.Equal(lastHorse, wonResolution.Score.Last().OwnedHorse);
    }
}