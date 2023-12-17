using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Chance.Effects;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;
using Derby.Engine.Race.Horses;
using Derby.Engine.Race.Ruleset.Variations.Derby2023;
using Derby.Engine.Race.Ruleset.Variations.Drechsler;

namespace Derby.Engine.Race.FunctionalTests.Tests.Ruleset.Variations.Derby2023;

public class Derby2023SimulationTests
{
    /// <summary>
    ///     Run full simulation of a game, based on the Derby 2023 ruleset. This test is used to verify that the game is working as expected.
    ///     Only validating the final horse location and whether they are eliminated,
    ///     sufficiently serves as a fingerprint, to ensure all internal mechanisms leading up to the final position
    ///     are working as expected.
    /// </summary>
    [Theory]
    [MemberData(nameof(Derby2023SimulationTestData))]
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

    public static IEnumerable<object[]> Derby2023SimulationTestData()
    {
        return new List<object[]>
            {
                // 2 horses, Isolde wins, Rusch eliminated.
                new object[]
                {
                    new List<string> { "Isolde", "Rusch" },
                    new GallopDeck(new List<GallopCard>
                    {
                        Derby2023GallopDeck.GetCard("Stærkt tempo!"),
                        Derby2023GallopDeck.GetCard("Omplacering i feltet"),
                        Derby2023GallopDeck.GetCard("Stærk fremrykning!"),
                        Derby2023GallopDeck.GetCard("Hesten er halt!"),
                        Derby2023GallopDeck.GetCard("Styrt!"),
                        Derby2023GallopDeck.GetCard("Fin galop")
                    }),
                    new ChanceDeck(new List<ChanceCard>
                    {
                        // None of the Derby 2023 Chance cards impacts the game, so they can all be omitted.
                        new ChanceCard { Title = "", Description = "", CardEffect = new NoEffect() },
                    }),
                    new List<(int, bool)> { (0, false), (5, true) }
                }
            };
    }
}