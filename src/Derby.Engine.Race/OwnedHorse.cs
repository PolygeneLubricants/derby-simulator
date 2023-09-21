using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race;

public class OwnedHorse
{
    public Player? Owner { get; init; }

    public Horse Horse { get; init; }
}