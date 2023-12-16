using Derby.Engine.Race.Board.Lanes;
using Derby.Engine.Race.Board.Lanes.PredefinedLanes;
using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;
using Derby.Engine.Race.FunctionalTests.Utilities.TestModels;

namespace Derby.Engine.Race.FunctionalTests.Tests.GallopEffects.Modifiers
{
    public class SteadyPaceModifierTests
    {
        [Fact]
        public void SteadyPaceModifier_WhenDrawn_SteadyPaceSkipGallopCards()
        {
            // Arrange
            var card = new ObservableGallopCard
            {
                Title = "",
                Description = "",
                CardEffect = new HomeStretchCompositeEffect(
                    new ModifierEffect(() => new MaxMoveModifier(4)),
                    new ModifierEffect(() => new SteadyPaceModifier(4)))
            };

            var chanceCard = new ObservableChanceCard
            {
                Title = "",
                Description = "",
                CardEffect = new Cards.Chance.Effects.NoEffect()
            };

            var builder = new RaceTestBuilder();
            var race =
                builder
                    .WithPredefinedLanes()
                    .WithHorseInRace(new[] { 2, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 2, out var _)
                    .WithGallopCard(card)
                    .WithChanceCard(chanceCard)
                    .Build();

            // Act
            var gallopCardDrawTimes = 0;
            card.OnDraw += () => { gallopCardDrawTimes++; };

            var chanceCardDrawTimes = 0;
            chanceCard.OnDraw += () => { chanceCardDrawTimes++; };
            _ = race.ResolveTurn(); // Moves 2, draw steady pace
            _ = race.ResolveTurn(); // Move 4
            _ = race.ResolveTurn(); // Move 4
            _ = race.ResolveTurn(); // Move 4
            _ = race.ResolveTurn(); // Move 4
            _ = race.ResolveTurn(); // Move 4

            // Assert
            Assert.Equal(1, gallopCardDrawTimes);
            Assert.Equal(1, chanceCardDrawTimes);
            Assert.Equal(22, race.State.RegisteredHorses[0].Location);
        }

        [Fact]
        public void SteadyPaceModifier_WhenDrawnInHomeStretch_MoveOnce()
        {
            // Arrange
            var card = new ObservableGallopCard
            {
                Title = "",
                Description = "",
                CardEffect = new HomeStretchCompositeEffect(
                    new ModifierEffect(() => new MaxMoveModifier(1)),
                    new ModifierEffect(() => new SteadyPaceModifier(1)))
            };

            var chanceCard = new ObservableChanceCard
            {
                Title = "",
                Description = "",
                CardEffect = new Cards.Chance.Effects.NoEffect()
            };

            var builder = new RaceTestBuilder();
            var race =
                builder
                    .WithLane(new Lane2Years().Fields, 2)
                    .WithHorseInRace(new[] { 26, 6, 10 }, 2, out var _)
                    .WithGallopCard(card)
                    .WithChanceCard(chanceCard)
                    .Build();

            // Act
            var gallopCardDrawTimes = 0;
            card.OnDraw += () => { gallopCardDrawTimes++; };

            _ = race.ResolveTurn(); // Moves to 26
            _ = race.ResolveTurn(); // Is home stretch, draw steady pace gallop card and move 1 and draw steady pace
            _ = race.ResolveTurn(); // IS home strech again, draw steady pace gallop card and move 1.

            // Assert
            Assert.Equal(3, gallopCardDrawTimes);
            Assert.Equal(28, race.State.RegisteredHorses[0].Location);
        }
    }
}