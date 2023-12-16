using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;
using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.FunctionalTests.Tests
{
    public class SimulationTests
    {
        /// <summary>
        ///     Run full simulation of a game. This test is used to verify that the game is working as expected.
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
                        DefaultGallopDeck.GetCard("Stærkt tempo"),
                        DefaultGallopDeck.GetCard("Protest"),
                        DefaultGallopDeck.GetCard("Hesten presses"),
                        DefaultGallopDeck.GetCard("Hesten går udemærket"),
                        DefaultGallopDeck.GetCard("Fin galop"),
                        DefaultGallopDeck.GetCard("Hesten forceres"),
                        DefaultGallopDeck.GetCard("Hesten kan ikke følge med"),
                        DefaultGallopDeck.GetCard("Styrt"),
                        DefaultGallopDeck.GetCard("Fin galop"),
                        DefaultGallopDeck.GetCard("Hesten falder tilbage"),
                        DefaultGallopDeck.GetCard("Stærk fremrykning")
                    }),
                    new ChanceDeck(new List<ChanceCard>
                    {
                        DefaultChanceDeck.GetCard("Forsikringsafgifter"),
                        DefaultChanceDeck.GetCard("Formueskat"),
                        DefaultChanceDeck.GetCard("Banen må repareres"),
                        DefaultChanceDeck.GetCard("Lottogevinst"),
                        DefaultChanceDeck.GetCard("Ekstra afgifter til Grand Prix og Derby"),
                        DefaultChanceDeck.GetCard("Overskud fra følauktionen"),
                        DefaultChanceDeck.GetCard("Staldtips"),
                        DefaultChanceDeck.GetCard("Ombygning af banen billigere end beregnet"),
                        DefaultChanceDeck.GetCard("Gør en forretning"),
                        DefaultChanceDeck.GetCard("Hesten halt"),
                        DefaultChanceDeck.GetCard("Vil de købe en hest?"),
                    }),
                    new List<(int, bool)> { (5, false), (7, false), (0, false), (11, false), (3, true) }
                },
                // 2 horses, Castor wins
                new object[]
                {
                    new List<string> { "Castor", "Whispering" },
                    new GallopDeck(new List<GallopCard>
                    {
                        DefaultGallopDeck.GetCard("Stærk fremrykning"),
                        DefaultGallopDeck.GetCard("Fin galop"),
                        DefaultGallopDeck.GetCard("Hesten falder tilbage"),
                        DefaultGallopDeck.GetCard("Fin galop"),
                    }),
                    new ChanceDeck(new List<ChanceCard>
                    {
                        DefaultChanceDeck.GetCard("Ekstra afgifter til Grand Prix og Derby"),
                        DefaultChanceDeck.GetCard("Fint regnskab")
                    }),
                    new List<(int, bool)> { (0, false), (9, false) }
                },
                // 3 horses, Vitesse wins, exciting ending
                new object[]
                {
                    new List<string> { "Vitesse", "Aldebaran", "Orkan" },
                    new GallopDeck(new List<GallopCard>
                    {
                        DefaultGallopDeck.GetCard("Fin galop"),
                        DefaultGallopDeck.GetCard("Fin galop"),
                        DefaultGallopDeck.GetCard("Protest"),
                        DefaultGallopDeck.GetCard("Fin galop"),
                        DefaultGallopDeck.GetCard("Pres ikke hesten for hårdt"),
                        DefaultGallopDeck.GetCard("Hesten falder tilbage"),
                        DefaultGallopDeck.GetCard("Hesten går udemærket"),
                    }),
                    new ChanceDeck(new List<ChanceCard>
                    {
                        DefaultChanceDeck.GetCard("Trænerafgifter"),
                        DefaultChanceDeck.GetCard("Ekstra afgifter til Grand Prix og Derby"),
                        DefaultChanceDeck.GetCard("Ombygning af banen billigere end beregnet"),
                        DefaultChanceDeck.GetCard("Banen skal bygges om"),
                        DefaultChanceDeck.GetCard("Staldtips"),
                    }),
                    new List<(int, bool)> { (0, false), (4, false), (2, false) }
                }
            };
        }
    }
}