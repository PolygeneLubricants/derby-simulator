using Derby.Engine.Race.Board.Lanes.Fields;
using Derby.Engine.Race.Cards.Chance.Effects;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;
using Derby.Engine.Race.FunctionalTests.Utilities.TestModels;

namespace Derby.Engine.Race.FunctionalTests.Tests.ChanceEffects;

public class NoEffectTests
{
    [Fact]
    public void NoEffect_WhenCardDrawn_NoEffect()
    {
        // Arrange
        var chanceCardWithTrigger = new ChanceCardWithDrawTrigger { Title = "", Description = "", CardEffect = new NoEffect() };
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(new List<IField> { new StartField(0), new ChanceField(100), new GoalField(200) }, 2)
            .WithHorseInRace(new[] { 1 }, out _)
            .WithChanceCard(chanceCardWithTrigger)
            .Build();

        // Act
        var drawnTimes = 0;
        chanceCardWithTrigger.OnDraw += delegate { drawnTimes++; };
        _ = race.ResolveTurn();

        // Assert
        Assert.Equal(1, drawnTimes);
    }

    [Fact]
    public void NoEffect_WhenCardDrawnTwice_NoEffect()
    {
        // Arrange
        var chanceCardWithTrigger = new ChanceCardWithDrawTrigger { Title = "", Description = "", CardEffect = new NoEffect() };
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(new List<IField> { new StartField(0), new ChanceField(100), new GoalField(200) }, 2)
            .WithHorseInRace(new[] { 1 }, out _)
            .WithHorseInRace(new[] { 1 }, out _)
            .WithChanceCard(chanceCardWithTrigger)
            .Build();

        // Act
        var drawnTimes = 0;
        chanceCardWithTrigger.OnDraw += delegate { drawnTimes++; };
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();

        // Assert
        Assert.Equal(2, drawnTimes);
    }

    [Fact]
    public void NoEffect_WhenDifferentCardsDrawnOnce_NoEffect()
    {
        // Arrange
        var chanceCardWithTrigger1 = new ChanceCardWithDrawTrigger { Title = "", Description = "", CardEffect = new NoEffect() };
        var chanceCardWithTrigger2 = new ChanceCardWithDrawTrigger { Title = "", Description = "", CardEffect = new NoEffect() };
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(new List<IField> { new StartField(0), new ChanceField(100), new GoalField(200) }, 2)
            .WithHorseInRace(new[] { 1 }, out _)
            .WithHorseInRace(new[] { 1 }, out _)
            .WithChanceCard(chanceCardWithTrigger1)
            .WithChanceCard(chanceCardWithTrigger2)
            .Build();

        // Act
        var drawnTimes1 = 0;
        var drawnTimes2 = 0;
        chanceCardWithTrigger1.OnDraw += delegate { drawnTimes1++; };
        chanceCardWithTrigger2.OnDraw += delegate { drawnTimes2++; };
        _ = race.ResolveTurn();
        _ = race.ResolveTurn();

        // Assert
        Assert.Equal(1, drawnTimes1);
        Assert.Equal(1, drawnTimes2);
    }
}