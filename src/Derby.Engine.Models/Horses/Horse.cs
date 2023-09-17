namespace Derby.Engine.Models.Horses;

public class Horse
{
    public required IList<int> Moves { get; init; }

    public required string Name { get; init; }

    public required int Years { get; init; }

    public required Color Color { get; init; }
}