using Derby.Engine.Race.Cards.Gallop;

namespace Derby.Engine.Race.Board.Lanes.Fields;

/// <summary>
///     When a horse lands on a Gallop field, it draw a <see cref="GallopCard" /> from the deck..
/// </summary>
public class GallopField : BaseField
{
    public GallopField(int tieBreaker) : base(tieBreaker)
    {
    }
}