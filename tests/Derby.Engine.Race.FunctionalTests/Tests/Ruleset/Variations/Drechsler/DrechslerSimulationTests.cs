using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;
using Derby.Engine.Race.Horses;
using Derby.Engine.Race.Ruleset.Variations.Drechsler;

namespace Derby.Engine.Race.FunctionalTests.Tests.Ruleset.Variations.Drechsler
{
    public class DrechslerSimulationTests
    {
        /// <summary>
        ///     Run full simulation of a game, based on the Drechsler ruleset. This test is used to verify that the game is working as expected.
        ///     Only validating the final horse location and whether they are eliminated,
        ///     sufficiently serves as a fingerprint, to ensure all internal mechanisms leading up to the final position
        ///     are working as expected.
        /// </summary>
        [Theory]
        [MemberData(nameof(SimulationTestData))]
        public void RunGame_WhenSimulatingRealGame_ShouldMatchExpectedResult(
            IList<string> horseNamesToRegister,
            GallopDeck gallopDeckInRace,
            ChanceDeck chanceDeckInRace,
            IList<(int, bool)> horsesFieldsFromGoalOrEliminated)
        {
            // Arrange
            var builder = new RaceTestBuilder();
            builder
                .WithPredefinedLanes()
                .WithGallopDeck(gallopDeckInRace)
                .WithChanceDeck(chanceDeckInRace);

            var horsesInRace = horseNamesToRegister.Select(HorseCollection.Get).Select(horse => new OwnedHorse { Horse = horse, Owner = null });
            builder.WithHorseInRace(horsesInRace);
            var race = builder.Build();

            // Act
            var turnLimit = 100_000;

            while (!race.GameEnded && turnLimit > race.State.CurrentTurn)
            {
                _ = race.ResolveTurn();
            }

            // Assert
            for (var i = 0; i < horsesFieldsFromGoalOrEliminated.Count; i++)
            {
                if (horsesFieldsFromGoalOrEliminated[i].Item2)
                {
                    Assert.True(race.State.RegisteredHorses[i].Eliminated);
                }
                else
                {
                    Assert.Equal(horsesFieldsFromGoalOrEliminated[i].Item1, race.State.RegisteredHorses[i].FieldsFromGoal);
                }
            }
        }

        public static IEnumerable<object[]> SimulationTestData()
        {
            return new List<object[]>
            {
                // 5 Horses, Whispering wins, Aldebaran eliminated.
                new object[]
                {
                    new List<string> { "Isolde", "Rusch", "Whispering", "Sweet Sue", "Aldebaran" },
                    new GallopDeck(new List<GallopCard>
                    {
                        DrechslerGallopDeck.GetCard("Stærkt tempo"),
                        DrechslerGallopDeck.GetCard("Protest"),
                        DrechslerGallopDeck.GetCard("Hesten presses"),
                        DrechslerGallopDeck.GetCard("Hesten går udemærket"),
                        DrechslerGallopDeck.GetCard("Fin galop"),
                        DrechslerGallopDeck.GetCard("Hesten forceres"),
                        DrechslerGallopDeck.GetCard("Hesten kan ikke følge med"),
                        DrechslerGallopDeck.GetCard("Styrt"),
                        DrechslerGallopDeck.GetCard("Fin galop"),
                        DrechslerGallopDeck.GetCard("Hesten falder tilbage"),
                        DrechslerGallopDeck.GetCard("Stærk fremrykning")
                    }),
                    new ChanceDeck(new List<ChanceCard>
                    {
                        DrechslerChanceDeck.GetCard("Forsikringsafgifter"),
                        DrechslerChanceDeck.GetCard("Formueskat"),
                        DrechslerChanceDeck.GetCard("Banen må repareres"),
                        DrechslerChanceDeck.GetCard("Lottogevinst"),
                        DrechslerChanceDeck.GetCard("Ekstra afgifter til Grand Prix og Derby"),
                        DrechslerChanceDeck.GetCard("Overskud fra følauktionen"),
                        DrechslerChanceDeck.GetCard("Staldtips"),
                        DrechslerChanceDeck.GetCard("Ombygning af banen billigere end beregnet"),
                        DrechslerChanceDeck.GetCard("Gør en forretning"),
                        DrechslerChanceDeck.GetCard("Hesten halt"),
                        DrechslerChanceDeck.GetCard("Vil de købe en hest?"),
                    }),
                    new List<(int, bool)> { (5, false), (7, false), (0, false), (11, false), (3, true) }
                },
                // 2 horses, Castor wins
                new object[]
                {
                    new List<string> { "Castor", "Whispering" },
                    new GallopDeck(new List<GallopCard>
                    {
                        DrechslerGallopDeck.GetCard("Stærk fremrykning"),
                        DrechslerGallopDeck.GetCard("Fin galop"),
                        DrechslerGallopDeck.GetCard("Hesten falder tilbage"),
                        DrechslerGallopDeck.GetCard("Fin galop"),
                    }),
                    new ChanceDeck(new List<ChanceCard>
                    {
                        DrechslerChanceDeck.GetCard("Ekstra afgifter til Grand Prix og Derby"),
                        DrechslerChanceDeck.GetCard("Fint regnskab")
                    }),
                    new List<(int, bool)> { (0, false), (9, false) }
                },
                // 3 horses, Vitesse wins, exciting ending
                new object[]
                {
                    new List<string> { "Vitesse", "Aldebaran", "Orkan" },
                    new GallopDeck(new List<GallopCard>
                    {
                        DrechslerGallopDeck.GetCard("Fin galop"),
                        DrechslerGallopDeck.GetCard("Fin galop"),
                        DrechslerGallopDeck.GetCard("Protest"),
                        DrechslerGallopDeck.GetCard("Fin galop"),
                        DrechslerGallopDeck.GetCard("Pres ikke hesten for hårdt"),
                        DrechslerGallopDeck.GetCard("Hesten falder tilbage"),
                        DrechslerGallopDeck.GetCard("Hesten går udemærket"),
                    }),
                    new ChanceDeck(new List<ChanceCard>
                    {
                        DrechslerChanceDeck.GetCard("Trænerafgifter"),
                        DrechslerChanceDeck.GetCard("Ekstra afgifter til Grand Prix og Derby"),
                        DrechslerChanceDeck.GetCard("Ombygning af banen billigere end beregnet"),
                        DrechslerChanceDeck.GetCard("Banen skal bygges om"),
                        DrechslerChanceDeck.GetCard("Staldtips"),
                    }),
                    new List<(int, bool)> { (0, false), (4, false), (2, false) }
                }
            };
        }
    }
}