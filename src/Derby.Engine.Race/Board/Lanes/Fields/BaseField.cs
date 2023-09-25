namespace Derby.Engine.Race.Board.Lanes.Fields;

/// <summary>
///     Base class for all fields.
/// </summary>
public abstract class BaseField : IField
{
    protected BaseField(int tieBreaker)
    {
        TieBreaker = tieBreaker;
    }

    public int TieBreaker { get; }
}