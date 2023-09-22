using Derby.Engine.Race.Board.Lanes;
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
                .WithLane(new Lane2Years().Fields, 2)
                .WithLane(new Lane3Years().Fields, 3)
                .WithLane(new Lane4Years().Fields, 4)
                .WithLane(new Lane5Years().Fields, 5)
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
                }
            };
        }
    }
}