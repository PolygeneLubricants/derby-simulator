using Derby.Engine.Race.Board.Lanes.Fields;

namespace Derby.Engine.Race.Board.Lanes;

public abstract class BaseLane : ILane
{
    protected BaseLane(IList<IField> fields)
    {
        Fields = fields;
    }

    public IList<IField> Fields { get; }
}