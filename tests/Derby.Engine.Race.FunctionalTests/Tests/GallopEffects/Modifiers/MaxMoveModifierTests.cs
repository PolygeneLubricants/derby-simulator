using Derby.Engine.Race.Board.Lanes;
using Derby.Engine.Race.Board.Lanes.PredefinedLanes;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;

namespace Derby.Engine.Race.FunctionalTests.Tests.GallopEffects.Modifiers
{
    public class MaxMoveModifierTests
    {
        [Fact]
        public void MaxMoveModifier_WhenDrawnInHomeStretch_MoveMax()
        {
            // Arrange
            var card = new GallopCard
            {
                Title = "",
                Description = "",
                CardEffect = new HomeStretchCompositeEffect(
                    new ModifierEffect(() => new MaxMoveModifier(2)), 
                    new NoEffect())
            };

            var builder = new RaceTestBuilder();
            var race =
                builder
                    .WithLane(new Lane2Years().Fields, 2)
                    .WithHorseInRace(new[] { 26, 6, 3 }, 2, out var _)
                    .WithGallopCard(card)
                    .WithNoEffectChanceCard()
                    .Build();

            // Act
            _ = race.ResolveTurn(); // Moves 26
            _ = race.ResolveTurn(); // Home stretch, draw max move, move 2
            _ = race.ResolveTurn(); // Move 3

            // Assert
            Assert.Equal(31, race.State.RegisteredHorses[0].Location);
        }

        [Fact]
        public void MaxMoveModifier_WhenDrawnNotInHomeStretch_NoEffect()
        {
            // Arrange
            var card = new GallopCard
            {
                Title = "",
                Description = "",
                CardEffect = new HomeStretchCompositeEffect(
                    new ModifierEffect(() => new MaxMoveModifier(2)), 
                    new NoEffect())
            };

            var builder = new RaceTestBuilder();
            var race =
                builder
                    .WithLane(new Lane2Years().Fields, 2)
                    .WithHorseInRace(new[] { 2, 6, 3 }, 2, out var _)
                    .WithGallopCard(card)
                    .WithNoEffectChanceCard()
                    .Build();

            // Act
            _ = race.ResolveTurn(); // Moves 2, draw max move, no effect.
            _ = race.ResolveTurn(); // Moves 6
            _ = race.ResolveTurn(); // Move 3

            // Assert
            Assert.Equal(11, race.State.RegisteredHorses[0].Location);
        }
    }
}