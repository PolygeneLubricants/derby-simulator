using Derby.Engine.Race.Horses;
using Derby.Engine.Race.Simulator.Loggers;
using Derby.Engine.Race.Simulator.Statistics;

namespace Derby.Engine.Race.Simulator;

public class Simulation
{
    public void Run(
        IList<string> horsesInRace)
    {
        var horsesToRace = MapHorse(horsesInRace);

        var race   = Race.GetDefault(horsesToRace);
        var logger = new GameLogLogger(race);

        var turnLimit = 100_000;

        while (!race.GameEnded && turnLimit > race.State.CurrentTurn)
        {
            var resolution = race.ResolveTurn();
            logger.Log(resolution);
        }
    }

    public void RunCombinations(CombinationRule rule)
    {
        var accumulator = new Accumulator();
        switch (rule)
        {
            case CombinationRule.All:
                RunCombinationsForAll(accumulator);
                break;
            default:
                throw new NotSupportedException("TBD");
        }
    }

    private void RunCombinationsForAll(Accumulator accumulator)
    {
        var horses = HorseCollection.Horses.Values;
        var cartesianProduct = 
            from first in horses  //        20
            from second in horses //       380
            from third in horses  //     6.840
            from fourth in horses //   116.280
            from fifth in horses  // 1.860.480
            where first  != second && first  != third  && first  != fourth && first  != fifth
            where second != third  && second != fourth && second != fifth
            where third  != fourth && third  != fifth
            where fourth != fifth
            select new []{ first, second, third, fourth, fifth };

        RunCombinations(accumulator, cartesianProduct);
    }

    private void RunCombinations(Accumulator accumulator, IEnumerable<Horse[]> cartesianProduct)
    {
        foreach (var horsesInRace in cartesianProduct)
        {
            RunCombinations(accumulator, horsesInRace);
        }

        var logger = new StatisticsLogger();
        logger.Log(accumulator);
    }

    private void RunCombinations(Accumulator accumulator, Horse[] cartesianProduct)
    {
        var race   = Race.GetDefault(cartesianProduct.Select(horse => new OwnedHorse { Horse = horse, Owner = null }));

        var turnLimit = 100_000;

        while (!race.GameEnded && turnLimit > race.State.CurrentTurn)
        {
            _ = race.ResolveTurn();
        }

        accumulator.AddResult(race.FinalResolution, race.State.GetScore());
    }

    private IEnumerable<OwnedHorse> MapHorse(IList<string> horses)
    {
        var stables = Enum.GetValues<StableCode>().ToList();
        for (var i = 0; i < horses.Count; i++)
        {
            yield return MapHorse(horses[i], stables[i % horses.Count]);
        }
    }

    private OwnedHorse MapHorse(string horse, StableCode stable)
    {
        return new OwnedHorse { Owner = new Player { Stable = new Stable { Code = stable } }, Horse = HorseCollection.Get(horse) };
    }
}