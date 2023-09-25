using Derby.Engine.Race.Board.Lanes;
using Derby.Engine.Race.Board.Lanes.PredefinedLanes;
using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;

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
                // 5 Horses, Sweet Sue wins.
                new object[]
                {
                    new List<string> { "Isolde", "Rusch", "Whispering", "Sweet Sue", "Aldebaran" },
                    new GallopDeck(new List<GallopCard>
                    {
                        DefaultGallopDeck.GetCard("Jævn fart"),
                        DefaultGallopDeck.GetCard("Hesten forceres"),
                        DefaultGallopDeck.GetCard("Hesten falder tilbage"),
                        DefaultGallopDeck.GetCard("Protest"),
                        DefaultGallopDeck.GetCard("Fin galop"),
                        DefaultGallopDeck.GetCard("Hesten kan ikke følge med"),
                        DefaultGallopDeck.GetCard("Fin galop"),
                        DefaultGallopDeck.GetCard("Stærk fremrykning"),
                        DefaultGallopDeck.GetCard("Fin galop"),
                        DefaultGallopDeck.GetCard("Styrt"),
                        DefaultGallopDeck.GetCard("Fin galop"),
                        DefaultGallopDeck.GetCard("Hesten går udemærket"),
                    }),
                    new ChanceDeck(new List<ChanceCard>
                    {
                        DefaultChanceDeck.GetCard("Stald-leje"),
                        DefaultChanceDeck.GetCard("Vil de købe en hest?"),
                        DefaultChanceDeck.GetCard("Gør en forretning"),
                        DefaultChanceDeck.GetCard("Trænerafgifter"),
                        DefaultChanceDeck.GetCard("Sælg en hest"),
                        DefaultChanceDeck.GetCard("Fint regnskab"),
                        DefaultChanceDeck.GetCard("Formueskat"),
                        DefaultChanceDeck.GetCard("Staldtips"),
                        DefaultChanceDeck.GetCard("Tag et lån"),
                    }),
                    new List<(int, bool)> { (3, false), (8, false), (6, false), (0, false), (10, false) }
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
                },
                // 10 horses, Isolde wins on Jævn Fart, and doesn't draw in HomeStretch (as intended).
                new object[]
                {
                    new List<string> { "Aldebaran", "Avalon", "Comet", "Castor", "Rigel", "Cassiopeja", "Tristan", "Isolde", "Rusch", "Caruso", },
                    new GallopDeck(new List<GallopCard>
                    {
                        DefaultGallopDeck.GetCard("Hesten falder tilbage"),
                        DefaultGallopDeck.GetCard("Styrt"),
                        DefaultGallopDeck.GetCard("Hesten presses"),
                        DefaultGallopDeck.GetCard("Protest"),
                        DefaultGallopDeck.GetCard("Fin galop"),
                        DefaultGallopDeck.GetCard("Stærkt tempo"),
                        DefaultGallopDeck.GetCard("Pres ikke hesten for hårdt"),
                        DefaultGallopDeck.GetCard("Stærk fremrykning"),
                        DefaultGallopDeck.GetCard("Svag slutspurt"),
                        DefaultGallopDeck.GetCard("Hesten kan ikke følge med"),
                        DefaultGallopDeck.GetCard("Jævn fart"),
                        DefaultGallopDeck.GetCard("Fin galop"),
                        DefaultGallopDeck.GetCard("Hesten forceres"),
                        DefaultGallopDeck.GetCard("Hesten kan ikke følge med"),
                        DefaultGallopDeck.GetCard("Hesten går udemærket"),
                        DefaultGallopDeck.GetCard("Fin galop"),
                        DefaultGallopDeck.GetCard("Fin galop"),
                        DefaultGallopDeck.GetCard("Omplacering i feltet"),
                        DefaultGallopDeck.GetCard("Fin galop"),
                        DefaultGallopDeck.GetCard("Hesten kan ikke følge med"),
                        DefaultGallopDeck.GetCard("Protest"),
                    }),
                    new ChanceDeck(new List<ChanceCard>
                    {
                        DefaultChanceDeck.GetCard("Enestående tilfælde"),
                        DefaultChanceDeck.GetCard("Staldtips"),
                        DefaultChanceDeck.GetCard("Staldtips"),
                        DefaultChanceDeck.GetCard("Staldtips"),
                        DefaultChanceDeck.GetCard("Banen må repareres"),
                        DefaultChanceDeck.GetCard("Sælg en hest"),
                        DefaultChanceDeck.GetCard("Tag et lån"),
                        DefaultChanceDeck.GetCard("Staldtips"),
                        DefaultChanceDeck.GetCard("Staldtips"),
                        DefaultChanceDeck.GetCard("Overskud fra følauktionen"),
                        DefaultChanceDeck.GetCard("Hesten halt"),
                        DefaultChanceDeck.GetCard("Sælg en hest"),
                        DefaultChanceDeck.GetCard("Gør en forretning"),
                        DefaultChanceDeck.GetCard("Ekstra afgifter til Grand Prix og Derby"),
                        DefaultChanceDeck.GetCard("Forsikringsafgifter"),
                        DefaultChanceDeck.GetCard("Køb en hest"),
                        DefaultChanceDeck.GetCard("Stald-leje"),
                        DefaultChanceDeck.GetCard("Vil de købe en hest?"),
                        DefaultChanceDeck.GetCard("Dårligt regnskab"),
                        DefaultChanceDeck.GetCard("Formueskat"),
                    }),
                    new List<(int, bool)> { (3, false), (1, false), (4, false), (6, false), (11, false), (3, true), (3, false), (0, false), (8, false), (23, true) }
                },
            };
        }
    }
}