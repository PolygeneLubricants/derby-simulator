﻿using Derby.Engine.Race.Board.Lanes;

namespace Derby.Engine.Race.Board;

/// <summary>
///     Collection of lanes this board contains.
///     A Derby board has 4 lanes, one for each age group.
/// </summary>
internal class LaneCollection
{
    public required ILane Lane2Years { get; set; }
    public required ILane Lane3Years { get; set; }
    public required ILane Lane4Years { get; set; }
    public required ILane Lane5Years { get; set; }
}