namespace Derby.Engine.Race.Board.Lanes.Fields;

public interface IField
{
    /// <summary>
    ///     Because of lane curvature in the original game,
    ///     some lanes with fewer fields are ahead of lanes with more fields,
    ///     even though the lane with more fields are further from start.
    ///     For example, in the first curve, field 7 in lane 2 is ahead of
    ///     field 7 in lane 3, 4 and 5.
    ///     Field 8 in lane 3, however, is ahead of field 8 in lane 4 and 5 (but behind field 8 in lane 2).
    /// </summary>
    public int TieBreaker { get; }
}