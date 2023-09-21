using Derby.Engine.Race.Board.Lanes.Fields;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;
using Derby.Engine.Race.Turns.Resolutions;

namespace Derby.Engine.Race.FunctionalTests.Tests.GallopEffects
{
    public class DisqualifyEffectTests
    {
        [Fact]
        public void DisqualifyEffect_AsHomeStretchCompositeEffect_AppliesInHomeStretch()
        {
            // Arrange
            var disqualifyCard = new GallopCard
            {
                Title = "",
                Description = "",
                CardEffect = new HomeStretchCompositeEffect(new DisqualifyEffect(), new NoEffect())
            };

            var builder = new RaceTestBuilder();
            var race = builder.WithLane(new List<IField>
                {
                    new StartField(0),
                    new NeutralField(100),
                    new GallopField(200),
                    new NeutralField(300),
                    new GoalField(500)
                }, 2)
                .WithHorseInRace(new[] { 4 }, out var eliminatedHorse)
                .WithGallopCard(disqualifyCard)
                .Build();

            // Act
            var disqualifiedResolution = race.ResolveTurn();

            // Assert
            Assert.IsType<HorseEliminatedTurnResolution>(disqualifiedResolution);
            var eliminatedTurnResolution = disqualifiedResolution as HorseEliminatedTurnResolution;
            Assert.Equal(eliminatedHorse, eliminatedTurnResolution.EliminatedHorse.OwnedHorse);

            Assert.True(race.State.GetScore().All(h => h.Eliminated));
        }

        [Fact]
        public void DisqualifyEffect_AsHomeStretchCompositeEffect_DoesNotApplyOutsideHomesStretch()
        {
            // Arrange
            var disqualifyCard = new GallopCard
            {
                Title = "",
                Description = "",
                CardEffect = new HomeStretchCompositeEffect(new DisqualifyEffect(), new NoEffect())
            };

            var builder = new RaceTestBuilder();
            var race = builder.WithLane(new List<IField>
                {
                    new StartField(0),
                    new NeutralField(100),
                    new GallopField(200),
                    new NeutralField(300),
                    new GoalField(500)
                }, 2)
                .WithHorseInRace(new[] { 3 }, out _)
                .WithGallopCard(disqualifyCard)
                .Build();

            // Act
            var disqualifiedResolution = race.ResolveTurn();

            // Assert
            Assert.IsType<EndTurnTurnResolution>(disqualifiedResolution);
            Assert.False(race.State.RegisteredHorses.First().Eliminated);
        }
    }
}