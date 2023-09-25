namespace Derby.Engine.Race.Horses;

public class OwnedHorse
{
    public Player? Owner { get; init; }

    public Horse Horse { get; init; }
}