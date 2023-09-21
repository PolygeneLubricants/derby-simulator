using Derby.Engine.Race.Simulator.Statistics;

namespace Derby.Engine.Race.Simulator.Loggers;

public class StatisticsLogger
{
    public void Log(Accumulator accumulator)
    {
        Console.WriteLine($"Total: {accumulator.Total}");
        Console.WriteLine($"Drawn: {accumulator.DrawnGames}");

        foreach (var result in accumulator.Results)
        {
            Console.WriteLine($"{result.Key} | 1:{result.Value.FirstPlaces} | 2:{result.Value.SecondPlaces} | 3:{result.Value.ThirdPlaces} | 4:{result.Value.FourthPlaces} | 5:{result.Value.FifthPlaces} | b:{result.Value.BelowFifthPlaces} | e:{result.Value.Eliminations}");
        }
    }
}