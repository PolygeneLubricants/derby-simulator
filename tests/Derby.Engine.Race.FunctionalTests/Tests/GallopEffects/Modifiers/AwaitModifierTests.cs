using Derby.Engine.Race.Board.Lanes;
using Derby.Engine.Race.Board.Lanes.Fields;
using Derby.Engine.Race.Board.Lanes.PredefinedLanes;
using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Chance.Effects;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;

namespace Derby.Engine.Race.FunctionalTests.Tests.GallopEffects.Modifiers
{
    public class AwaitModifierTests
    {
        [Fact]
        public void AwaitModifier_AsGallopCardWithAll_BehaveAsGallopCard()
        {
            // Arrange
            var card = new GallopCard
            {
                Title = "",
                Description = "",
                CardEffect = new HomeStretchCompositeEffect(new EndTurnAndSkipEffect(),
                    new ModifierEffect(() => new AwaitModifier(AwaitType.All)))
            };

            var builder = new RaceTestBuilder();
            var race =
                builder
                    .WithLane(new Lane2Years().Fields, 2)
                    .WithHorseInRace(new[] { 6, 1, 1, 1 }, 2, out _)
                    .WithHorseInRace(new[] { 7, 4, 2, 1 }, 2, out _)
                    .WithHorseInRace(new[] { 1, 3, 2, 1 }, 2, out _)
                    .WithGallopCard(card)
                    .WithNoEffectGallopCard()
                    .WithNoEffectChanceCard()
                    .Build();

            // Act
            _ = race.ResolveTurn(); // Move to 6. Pick up await all gallop
            _ = race.ResolveTurn(); // Move 7 (ahead)
            _ = race.ResolveTurn(); // Move to 1

            _ = race.ResolveTurn(); // Wait
            _ = race.ResolveTurn(); // Move to 11
            _ = race.ResolveTurn(); // Move to 4

            _ = race.ResolveTurn(); // Wait
            _ = race.ResolveTurn(); // Move to 13
            _ = race.ResolveTurn(); // Move to 6 pick up no effect gallup

            _ = race.ResolveTurn(); // Wait
            _ = race.ResolveTurn(); // Move to 14
            _ = race.ResolveTurn(); // Move to 7

            _ = race.ResolveTurn(); // Move to 7

            // Assert
            Assert.Equal(13, race.State.RegisteredHorses[0].Location);
            Assert.Equal(14, race.State.RegisteredHorses[1].Location);
            Assert.Equal(7, race.State.RegisteredHorses[2].Location);
        }

        [Fact]
        public void AwaitModifier_AsGallopCardWithNearest_BehaveAsGallopCard()
        {
            // Arrange
            var card = new GallopCard
            {
                Title = "",
                Description = "",
                CardEffect = new HomeStretchCompositeEffect(new EndTurnAndSkipEffect(),
                    new ModifierEffect(() => new AwaitModifier(AwaitType.Nearest)))
            };

            var builder = new RaceTestBuilder();
            var race =
                builder
                    .WithLane(new Lane2Years().Fields, 2)
                    .WithHorseInRace(new[] { 6, 1, 1, 1 }, 2, out _)
                    .WithHorseInRace(new[] { 7, 4, 2, 1 }, 2, out _)
                    .WithHorseInRace(new[] { 1, 3, 2, 1 }, 2, out _)
                    .WithGallopCard(card)
                    .WithNoEffectGallopCard()
                    .WithNoEffectChanceCard()
                    .Build();

            // Act
            _ = race.ResolveTurn(); // Move to 6. Pick up await all gallop
            _ = race.ResolveTurn(); // Move 7 (ahead)
            _ = race.ResolveTurn(); // Move to 1

            _ = race.ResolveTurn(); // Move to 7
            _ = race.ResolveTurn(); // Move to 11
            _ = race.ResolveTurn(); // Move to 4

            // Assert
            Assert.Equal(7, race.State.RegisteredHorses[0].Location);
            Assert.Equal(11, race.State.RegisteredHorses[1].Location);
            Assert.Equal(4, race.State.RegisteredHorses[2].Location);
        }

        [Fact]
        public void AwaitModifier_AsGallopCardWitLast_BehaveAsGallopCard()
        {
            // Arrange
            var card = new GallopCard
            {
                Title = "",
                Description = "",
                CardEffect = new HomeStretchCompositeEffect(new EndTurnAndSkipEffect(),
                    new ModifierEffect(() => new AwaitModifier(AwaitType.Last)))
            };

            var builder = new RaceTestBuilder();
            var race =
                builder
                    .WithLane(new Lane2Years().Fields, 2)
                    .WithHorseInRace(new[] { 2, 4, 2, 1 }, 2, out _)
                    .WithHorseInRace(new[] { 6, 1, 1, 1 }, 2, out _)
                    .WithHorseInRace(new[] { 1, 3, 2, 1 }, 2, out _)
                    .WithNoEffectGallopCard()
                    .WithGallopCard(card)
                    .WithNoEffectGallopCard()
                    .WithNoEffectGallopCard()
                    .WithNoEffectChanceCard()
                    .Build();

            // Act
            _ = race.ResolveTurn(); // Move to 2 and pick up no effect Gallop
            _ = race.ResolveTurn(); // Move 6 and pick up Gallop card to wait.
            _ = race.ResolveTurn(); // Move to 1

            _ = race.ResolveTurn(); // Move to 6
            _ = race.ResolveTurn(); // Wait
            _ = race.ResolveTurn(); // Move to 4

            _ = race.ResolveTurn(); // Move to 8
            _ = race.ResolveTurn(); // Wait
            _ = race.ResolveTurn(); // Move to 6

            _ = race.ResolveTurn(); // Move to 9
            _ = race.ResolveTurn(); // Move to 7
            _ = race.ResolveTurn(); // Move to 7

            // Assert
            Assert.Equal(9, race.State.RegisteredHorses[0].Location);
            Assert.Equal(7, race.State.RegisteredHorses[1].Location);
            Assert.Equal(7, race.State.RegisteredHorses[2].Location);
        }

        [Fact]
        public void AwaitModifier_AwaitWhenAwaitedHorseIsEliminated_AwaitEffectRemoved()
        {
            // Arrange
            var card = new GallopCard
            {
                Title = "",
                Description = "",
                CardEffect = new ModifierEffect(() => new AwaitModifier(AwaitType.Last))
            };

            var eliminate = new ChanceCard()
            {
                Title = "",
                Description = "",
                CardEffect = new EliminateHorseEffect()
            };

            var builder = new RaceTestBuilder();
            var race =
                builder
                    .WithLane(new List<IField>
                    {
                        new StartField(0), 
                        new ChanceField(100), 
                        new NeutralField(200),
                        new GallopField(300),
                        new NeutralField(400),
                        new GoalField(500)
                    }, 2)
                    .WithHorseInRace(new[] { 3, 10 , 1 }, 2, out _)
                    .WithHorseInRace(new[] { 2, 1, 1 }, 2, out _)
                    .WithHorseInRace(new[] { 1, 10, 1 }, 2, out _)
                    .WithGallopCard(card)
                    .WithNoEffectGallopCard()
                    .WithChanceCard(eliminate)
                    .Build();

            // Act
            _ = race.ResolveTurn(); // Move to 3, pick up await
            _ = race.ResolveTurn(); // Move to 2
            _ = race.ResolveTurn(); // Move to 1 gets eliminated

            _ = race.ResolveTurn(); // Wait, as horse is still behind, even though one has been eliminated.
            _ = race.ResolveTurn(); // Move to 3, Draw no effect Gallop card
                                    // Third horse eliminated, no longer moves.

            _ = race.ResolveTurn(); // Move to 4
            _ = race.ResolveTurn(); // Move to 4, no longer waits.

            // Assert
            Assert.Equal(4, race.State.RegisteredHorses[0].Location);
            Assert.Equal(4, race.State.RegisteredHorses[1].Location);
            Assert.True(race.State.RegisteredHorses[2].Eliminated);
        }
    }
}