namespace Derby.Engine.Race.Board.Lanes.Fields;

/// <summary>
///     When a horse lands on a chance field, it draws a <see cref="ChanceCard" /> from the deck.
/// </summary>
public class ChanceField : BaseField
{
    public ChanceField(int tieBreaker) : base(tieBreaker)
    {
    }
}