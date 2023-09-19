using Derby.Engine.Race.Board.Lanes;
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
                    new MoveEffect(4),
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
                    .WithLane(new Lane2Years().Fields, 2)
                    .WithLane(new Lane3Years().Fields, 3)
                    .WithLane(new Lane4Years().Fields, 4)
                    .WithLane(new Lane5Years().Fields, 5)
                    .WithHorseInRace(new[] { 2, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 2, out var _)
                    .WithGallopCard(card)
                    .WithChanceCard(chanceCard)
                    .Build();

            // Act
            var gallopCardDrawTimes = 0;
            card.OnDraw += () => { gallopCardDrawTimes++; };

            var chanceCardDrawTimes = 0;
            chanceCard.OnDraw += () => { chanceCardDrawTimes++; };
            _ = race.ResolveTurn();
            _ = race.ResolveTurn();
            _ = race.ResolveTurn();
            _ = race.ResolveTurn();
            _ = race.ResolveTurn();
            _ = race.ResolveTurn();

            // Assert
            Assert.Equal(1, gallopCardDrawTimes);
            Assert.Equal(1, chanceCardDrawTimes);
            Assert.Equal(22, race.State.HorsesInRace[0].Location);
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
                    new MoveEffect(1),
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
            _ = race.ResolveTurn(); // Moves 1

            // Assert
            Assert.Equal(2, gallopCardDrawTimes);
            Assert.Equal(28, race.State.HorsesInRace[0].Location);
        }
    }
}