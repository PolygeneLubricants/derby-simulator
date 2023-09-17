using Derby.Engine.Models.Horses;

namespace Derby.Engine.Models;

public class OwnedHorse
{
    public Player Owner { get; init; }

    public Horse Horse { get; init; }
}