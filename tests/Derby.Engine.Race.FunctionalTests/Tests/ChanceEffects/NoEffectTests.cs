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
        var race = builder.WithLane(new List<IField> { new StartField(), new ChanceField(), new GoalField() }, 2)
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
}