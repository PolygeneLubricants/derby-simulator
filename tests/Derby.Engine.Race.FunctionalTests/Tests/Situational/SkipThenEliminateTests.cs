using Derby.Engine.Race.Board.Lanes;
using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;
using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Chance.Effects;

namespace Derby.Engine.Race.FunctionalTests.Tests.Situational
{
    public class SkipThenEliminateTests
    {
        [Fact]
        public void SkipThenEliminate_WhenHorseDrawsNonApplicableSkipThenOtherHorseIsEliminated_HorseNotSkipped()
        {
            // Arrange
            var force     = new GallopCard { Title = "Hesten forceres", Description = "Flyt 4 felter ekstra frem.", CardEffect = new MoveEffect(4) };
            var skip      = new GallopCard { Title = "Pres ikke hesten for hårdt", Description = "(Gælder kun i opløbet) Stå over en omgang.", CardEffect = new HomeStretchCompositeEffect(new EndTurnAndSkipEffect(), new Derby.Engine.Race.Cards.Gallop.Effects.NoEffect()) };
            var eliminate = new ChanceCard { Title = "Hesten halt", Description = "Tag den ud af løbet og sælg den til indehaveren af A/S Hesteagenturet for 500 kr. Er selskabet ikke solgt, sælges hesten til banken for 500 kr.", CardEffect = new EliminateHorseEffect() };

            var builder = new RaceTestBuilder();
            var race =
                builder
                    .WithLane(new Lane2Years().Fields, 2)
                    .WithLane(new Lane3Years().Fields, 3)
                    .WithLane(new Lane4Years().Fields, 4)
                    .WithLane(new Lane5Years().Fields, 5)
                    .WithHorseInRace(new[] { 1, 2, 3, 3, 4, 2, 4, 3, 1, 3, 2, 4 }, 2, out _)
                    .WithHorseInRace(new[] { 5, 2, 1, 3, 4, 1, 5, 2, 4, 3, 5, 1 }, 3, out _)
                    .WithHorseInRace(new[] { 6, 1, 3, 3, 5, 5, 2, 2, 4, 4, 6, 1 }, 4, out _)
                    .WithHorseInRace(new[] { 6, 6, 6, 1, 3, 6, 4, 5, 6, 2, 3, 4 }, 5, out _)
                    .WithHorseInRace(new[] { 1, 1, 4, 6, 5, 5, 5, 5, 5, 5, 5, 1 }, 5, out _)
                    .WithGallopCard(force)
                    .WithChanceCard(eliminate)
                    .WithGallopCard(skip)
                    .WithNoEffectChanceCard()
                    .WithNoEffectGallopCard()
                    .Build();

            // Act
            _ = race.ResolveTurn(); // move 1
            _ = race.ResolveTurn(); // move 5
            _ = race.ResolveTurn(); // move 6, force 4
            _ = race.ResolveTurn(); // move 6, skips if home stretch (is not)
            _ = race.ResolveTurn(); // move 1

            _ = race.ResolveTurn(); // move 2
            _ = race.ResolveTurn(); // move 2
            _ = race.ResolveTurn(); // move 1, eliminated
            _ = race.ResolveTurn(); // move 6, draw chance
            _ = race.ResolveTurn(); // move 1

            // Assert
            Assert.Equal(3, race.State.RegisteredHorses[0].Location);
            Assert.Equal(7, race.State.RegisteredHorses[1].Location);
            Assert.True(race.State.RegisteredHorses[2].Eliminated);
            Assert.Equal(12, race.State.RegisteredHorses[3].Location);
            Assert.Equal(2, race.State.RegisteredHorses[4].Location);
        }

        [Fact]
        public void SkipThenEliminate_WhenHorseDrawsNonApplicableSkipThenOtherHorseIsEliminatedAndIsLast_NewRoundBegins()
        {
            // Arrange
            var force = new GallopCard { Title = "Hesten forceres", Description = "Flyt 4 felter ekstra frem.", CardEffect = new MoveEffect(4) };
            var skip = new GallopCard { Title = "Pres ikke hesten for hårdt", Description = "(Gælder kun i opløbet) Stå over en omgang.", CardEffect = new HomeStretchCompositeEffect(new EndTurnAndSkipEffect(), new Derby.Engine.Race.Cards.Gallop.Effects.NoEffect()) };
            var eliminate = new ChanceCard { Title = "Hesten halt", Description = "Tag den ud af løbet og sælg den til indehaveren af A/S Hesteagenturet for 500 kr. Er selskabet ikke solgt, sælges hesten til banken for 500 kr.", CardEffect = new EliminateHorseEffect() };

            var builder = new RaceTestBuilder();
            var race =
                builder
                    .WithLane(new Lane2Years().Fields, 2)
                    .WithLane(new Lane3Years().Fields, 3)
                    .WithLane(new Lane4Years().Fields, 4)
                    .WithHorseInRace(new[] { 1, 2, 3, 3, 4, 2, 4, 3, 1, 3, 2, 4 }, 2, out _)
                    .WithHorseInRace(new[] { 5, 2, 1, 3, 4, 1, 5, 2, 4, 3, 5, 1 }, 3, out _)
                    .WithHorseInRace(new[] { 6, 1, 3, 3, 5, 5, 2, 2, 4, 4, 6, 1 }, 4, out _)
                    .WithGallopCard(force)
                    .WithChanceCard(eliminate)
                    .WithNoEffectGallopCard()
                    .Build();

            // Act
            _ = race.ResolveTurn(); // move 1
            _ = race.ResolveTurn(); // move 5
            _ = race.ResolveTurn(); // move 6, force 4

            _ = race.ResolveTurn(); // move 2
            _ = race.ResolveTurn(); // move 2
            _ = race.ResolveTurn(); // move 1, eliminated

            _ = race.ResolveTurn(); // move 3
            _ = race.ResolveTurn(); // move 1

            // Assert
            Assert.Equal(6, race.State.RegisteredHorses[0].Location);
            Assert.Equal(8, race.State.RegisteredHorses[1].Location);
            Assert.True(race.State.RegisteredHorses[2].Eliminated);
        }
    }
}
