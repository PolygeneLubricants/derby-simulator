﻿using Derby.Engine.Race.Board.Lanes;
using Derby.Engine.Race.Board.Lanes.Fields;

namespace Derby.Engine.Race.FunctionalTests.Utilities.TestModels;

public class CustomLane : BaseLane
{
    public CustomLane(IList<IField> fields) : base(fields)
    {
    }
}