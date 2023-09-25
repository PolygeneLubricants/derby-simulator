namespace Derby.Engine.Race.Board.Lanes.Fields;

/// <summary>
///     Represents a neutral field. This field does nothing when the horse lands on it.
/// </summary>
public class NeutralField : BaseField
{
    public NeutralField(int tieBreaker) : base(tieBreaker)
    {
    }
}