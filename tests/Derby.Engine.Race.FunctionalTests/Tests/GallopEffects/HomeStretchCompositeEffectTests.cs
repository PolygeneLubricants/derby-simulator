using Derby.Engine.Race.Board.Lanes.Fields;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;
using Derby.Engine.Race.FunctionalTests.Utilities.TestModels;

namespace Derby.Engine.Race.FunctionalTests.Tests.GallopEffects;

public class HomeStretchCompositeEffectTests
{
    [Fact]
    public void HomeStretchCompositeEffect_WhenInHomeStretch_HomeStretchInvoked()
    {
        // Arrange
        var homeStretchEffect = new ObservableGallopCardEffect();
        var notHomeStretchEffect = new ObservableGallopCardEffect();
        var card1 = new GallopCard { Title = "", Description = "", CardEffect = new HomeStretchCompositeEffect(homeStretchEffect, notHomeStretchEffect) };
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(new List<IField> { new StartField(0), new GallopField(100), new GoalField(200) }, 2)
            .WithHorseInRace(new[] { 2 }, out _)
            .WithGallopCard(card1)
            .Build();

        // Act
        var homeStretchActivatedTimes = 0;
        var notHomeStretchActivatedTimes = 0;
        homeStretchEffect.OnResolve += delegate { homeStretchActivatedTimes++; };
        notHomeStretchEffect.OnResolve += delegate { notHomeStretchActivatedTimes++; };
        _ = race.ResolveTurn();

        // Assert
        Assert.Equal(1, homeStretchActivatedTimes);
        Assert.Equal(0, notHomeStretchActivatedTimes);
    }

    [Fact]
    public void HomeStretchCompositeEffect_WhenNotInHomeStretch_HomeStretchNotInvoked()
    {
        // Arrange
        var homeStretchEffect = new ObservableGallopCardEffect();
        var notHomeStretchEffect = new ObservableGallopCardEffect();
        var card1 = new GallopCard { Title = "", Description = "", CardEffect = new HomeStretchCompositeEffect(homeStretchEffect, notHomeStretchEffect) };
        var builder = new RaceTestBuilder();
        var race = builder.WithLane(new List<IField> { new StartField(0), new GallopField(100), new GoalField(200) }, 2)
            .WithHorseInRace(new[] { 1 }, out _)
            .WithGallopCard(card1)
            .Build();

        // Act
        var homeStretchActivatedTimes = 0;
        var notHomeStretchActivatedTimes = 0;
        homeStretchEffect.OnResolve += delegate { homeStretchActivatedTimes++; };
        notHomeStretchEffect.OnResolve += delegate { notHomeStretchActivatedTimes++; };
        _ = race.ResolveTurn();

        // Assert
        Assert.Equal(0, homeStretchActivatedTimes);
        Assert.Equal(1, notHomeStretchActivatedTimes);
    }
}