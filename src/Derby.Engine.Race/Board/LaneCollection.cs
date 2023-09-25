using Derby.Engine.Race.Board.Lanes;

namespace Derby.Engine.Race.Board;

public class LaneCollection
{
    public required ILane Lane2Years { get; set; }
    public required ILane Lane3Years { get; set; }
    public required ILane Lane4Years { get; set; }
    public required ILane Lane5Years { get; set; }
}