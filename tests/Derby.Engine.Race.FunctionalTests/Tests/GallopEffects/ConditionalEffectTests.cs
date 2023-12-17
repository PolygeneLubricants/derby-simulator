using Derby.Engine.Race.Board.Lanes.Fields;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;

namespace Derby.Engine.Race.FunctionalTests.Tests.GallopEffects;

public class ConditionalEffectTests
{
    [Fact]
    public void Conditional_WhenHorseIsLeader_EffectDoesNotTrigger()
    {
        // Arrange
        var card1   = new GallopCard { Title = "", Description = "", CardEffect = new ConditionalEffect(Condition.IsNotLeader, new MoveEffect(2)) };
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

    [Fact]
    public void Conditional_WhenHorseIsNotLeader_EffectTriggers()
    {
        // Arrange
        var card1   = new GallopCard { Title = "", Description = "", CardEffect = new ConditionalEffect(Condition.IsNotLeader, new MoveEffect(2)) };
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(new List<IField>
            {
                new StartField(0),
                new GallopField(100),
                new NeutralField(200),
                new GoalField(300)
            }, 2)
            .WithHorseInRace(new[] { 2 }, out _)
            .WithHorseInRace(new[] { 1 }, out _)
            .WithGallopCard(card1)
            .Build();

        // Act
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();

        // Assert
        Assert.Equal(2, race.State.RegisteredHorses[0].Location);
        Assert.Equal(3, race.State.RegisteredHorses[1].Location);
    }
}