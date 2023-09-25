using Derby.Engine.Race;
using Derby.Engine.Race.Horses;
using Derby.Simulator.Loggers;
using Derby.Simulator.Statistics;

namespace Derby.Simulator;

public class Simulation
{
    public void RunRandom(int horseCount)
    {
        if (horseCount is <= 0 or > 10)
        {
            throw new ArgumentException(horseCount.ToString());
        }

        var horsesInRace = HorseCollection.Horses.Values.OrderBy(_ => Guid.NewGuid()).Take(horseCount).Select(h => h.Name).ToList();
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
    
    public void Run(
        IList<string>? p1Horses,
        IList<string>? p2Horses,
        IList<string>? p3Horses,
        IList<string>? p4Horses,
        IList<string>? p5Horses)
    {
        IList<OwnedHorse> horsesInRace = new List<OwnedHorse>();
        if (p1Horses != null && p1Horses.Any())
        {
            var horses = p1Horses.Select(horse => MapHorse(horse, StableCode.A));
            horsesInRace = horsesInRace.Concat(horses).ToList();
        }
        if (p2Horses != null && p2Horses.Any())
        {
            var horses = p2Horses.Select(horse => MapHorse(horse, StableCode.B));
            horsesInRace = horsesInRace.Concat(horses).ToList();
        }
        if (p3Horses != null && p3Horses.Any())
        {
            var horses = p3Horses.Select(horse => MapHorse(horse, StableCode.C));
            horsesInRace = horsesInRace.Concat(horses).ToList();
        }
        if (p4Horses != null && p4Horses.Any())
        {
            var horses = p4Horses.Select(horse => MapHorse(horse, StableCode.D));
            horsesInRace = horsesInRace.Concat(horses).ToList();
        }
        if (p5Horses != null && p5Horses.Any())
        {
            var horses = p5Horses.Select(horse => MapHorse(horse, StableCode.E));
            horsesInRace = horsesInRace.Concat(horses).ToList();
        }

        var race   = Race.GetDefault(horsesInRace);
        var logger = new GameLogLogger(race);

        var turnLimit = 100_000;

        while (!race.GameEnded && turnLimit > race.State.CurrentTurn)
        {
            var resolution = race.ResolveTurn();
            logger.Log(resolution);
        }
    }

    public void RunCombinations(CombinationMode mode, int raceSize, int iterations)
    {
        var accumulator = new Accumulator();
        switch (mode)
        {
            case CombinationMode.All:
                RunCombinations(accumulator, raceSize, iterations, HorseCollection.Horses.Values);
                break;
            case CombinationMode.TwoYears:
                RunCombinations(accumulator, raceSize, iterations, HorseCollection.Horses.Values.Where(h => h.Years == 2).ToList());
                break;

            case CombinationMode.ThreeYears:
                RunCombinations(accumulator, raceSize, iterations, HorseCollection.Horses.Values.Where(h => h.Years == 3).ToList());
                break;
            case CombinationMode.FourYears:
                RunCombinations(accumulator, raceSize, iterations, HorseCollection.Horses.Values.Where(h => h.Years == 4).ToList());
                break;
            case CombinationMode.FiveYears:
                RunCombinations(accumulator, raceSize, iterations, HorseCollection.Horses.Values.Where(h => h.Years == 5).ToList());
                break;
            default:
                throw new NotSupportedException("TBD");
        }
    }

    private void RunCombinations(Accumulator accumulator, int raceSize, int iterations, ICollection<Horse> horsesToCombine)
    {
        if (raceSize is < 1 or > 5)
        {
            throw new ArgumentException(raceSize.ToString());
        }

        var cartesianProduct = GetCartesianProduct(horsesToCombine, raceSize);
        RunCombinations(accumulator, cartesianProduct, iterations);
    }

    private IEnumerable<Horse[]> GetCartesianProduct(ICollection<Horse> horsesToCombine, int raceSize)
    {
        switch (raceSize)
        {
            case 1:
                return from first in horsesToCombine //        20
                    select new[] { first };
            case 2:
                return from first in horsesToCombine //        20
                    from second in horsesToCombine   //       380
                    where first != second
                    select new[] { first, second };
            case 3:
                return from first in horsesToCombine //        20
                    from second in horsesToCombine   //       380
                    from third in horsesToCombine    //     6.840
                    where first != second && first != third
                    where second != third
                    select new[] { first, second, third };
            case 4:
                return from first in horsesToCombine //        20
                    from second in horsesToCombine   //       380
                    from third in horsesToCombine    //     6.840
                    from fourth in horsesToCombine   //   116.280
                    where first != second && first != third && first != fourth
                    where second != third && second != fourth
                    where third != fourth
                    select new[] { first, second, third, fourth };
            case 5:
                return from first in horsesToCombine //        20
                    from second in horsesToCombine   //       380
                    from third in horsesToCombine    //     6.840
                    from fourth in horsesToCombine   //   116.280
                    from fifth in horsesToCombine    // 1.860.480
                    where first != second && first != third && first != fourth && first != fifth
                    where second != third && second != fourth && second != fifth
                    where third != fourth && third != fifth
                    where fourth != fifth
                    select new[] { first, second, third, fourth, fifth };
            default:
                throw new NotSupportedException();
        }
    }

    private void RunCombinations(Accumulator accumulator, IEnumerable<Horse[]> cartesianProduct, int iterations)
    {
        for (var i = 0; i < iterations; i++)
        {
            foreach (var horsesInRace in cartesianProduct)
            {
                RunCombinations(accumulator, horsesInRace);
            }
        }

        var logger = new StatisticsLogger();
        logger.Log(accumulator);
    }

    private void RunCombinations(Accumulator accumulator, Horse[] horsesInRace)
    {
        var race   = Race.GetDefault(horsesInRace.Select(horse => new OwnedHorse { Horse = horse, Owner = null }));
        
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
            yield return MapHorse(horses[i], stables[i % stables.Count]);
        }
    }

    private OwnedHorse MapHorse(string horse, StableCode stable)
    {
        return new OwnedHorse { Owner = new Player { Stable = new Stable { Code = stable } }, Horse = HorseCollection.Get(horse) };
    }
}