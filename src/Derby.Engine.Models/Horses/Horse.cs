namespace Derby.Engine.Models.Horses;

public class Horse
{
    public required IList<int> Moves { get; init; }

    public required string Name { get; init; }

    public required int Years { get; init; }

    public required Color Color { get; init; }

    public int GetMoves(int turn)
    {
        var nextMoves = turn % Moves.Count; // Wrap around if turn is more than max moves.
        return Moves[nextMoves];
    }
}