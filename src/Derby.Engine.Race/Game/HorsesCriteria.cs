using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Game;

public class HorsesCriteria
{
    public HorsesCriteria(Func<Horse, bool> criteria)
    {
        Criteria = criteria;
    }

    public Func<Horse, bool> Criteria { get; init; }
}