using Derby.Engine.Race.Cards.Chance.Effects;
using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;

namespace Derby.Engine.Race.FunctionalTests.Tests.Situational
{
    public class EliminateLastTest
    {
        [Fact]
        public void EliminateLast_WhenLastHorseEliminated_FirstHorseTurn()
        {
            // Arrange
            var eliminate = new ChanceCard { Title = "Hesten halt", Description = "Tag den ud af løbet og sælg den til indehaveren af A/S Hesteagenturet for 500 kr. Er selskabet ikke solgt, sælges hesten til banken for 500 kr.", CardEffect = new EliminateHorseEffect() };

            var builder = new RaceTestBuilder();
            var race =
                builder
                    .WithPredefinedLanes()
                    .WithHorseInRace(new[] { 1, 2, 2  , 5 }, 2, out _)
                    .WithHorseInRace(new[] { 3, 3, 3  , 6 }, 3, out _)
                    .WithHorseInRace(new[] { 5, 6, 100, 1 }, 4, out _)
                    .WithNoEffectGallopCard()
                    .WithChanceCard(eliminate)
                    .WithNoEffectChanceCard()
                    .Build();

            // Act
            _ = race.ResolveTurn(); // move to 1
            _ = race.ResolveTurn(); // move to 3
            _ = race.ResolveTurn(); // move to 5

            _ = race.ResolveTurn(); // move to 3
            _ = race.ResolveTurn(); // move to 6, draw no effect gallop
            _ = race.ResolveTurn(); // move to 11, draw eliminate

            _ = race.ResolveTurn(); // move 5
            _ = race.ResolveTurn(); // move 9
                                    // Eliminated

            _ = race.ResolveTurn(); // move 10
            _ = race.ResolveTurn(); // move 15

            // Assert
            Assert.Equal(10, race.State.RegisteredHorses[0].Location);
            Assert.Equal(15, race.State.RegisteredHorses[1].Location);
            Assert.True(race.State.RegisteredHorses[2].Eliminated);
        }
    }
}
