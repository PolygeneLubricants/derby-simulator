using Derby.Simulator.Statistics;

namespace Derby.Simulator.Loggers;

public class StatisticsLogger
{
    public void Log(Accumulator accumulator)
    {
        Console.WriteLine($"Total: {accumulator.Total}");
        Console.WriteLine($"Drawn: {accumulator.DrawnGames}");

        Console.WriteLine("Name,1st,2nd,3rd,4th,5th,<5th,Eliminations,Gallop cards Drawn,Chance cards drawn");

        foreach (var result in accumulator.Results)
        {
            Console.WriteLine($"{result.Key},{result.Value.FirstPlaces},{result.Value.SecondPlaces},{result.Value.ThirdPlaces},{result.Value.FourthPlaces},{result.Value.FifthPlaces},{result.Value.BelowFifthPlaces},{result.Value.Eliminations},{result.Value.GallopCardsDrawn},{result.Value.ChanceCardsDrawn}");
        }
    }
}