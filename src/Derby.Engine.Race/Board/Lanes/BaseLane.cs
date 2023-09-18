using Derby.Engine.Race.Board.Lanes.Fields;

namespace Derby.Engine.Race.Board.Lanes;

public abstract class BaseLane : ILane
{
    protected BaseLane()
    {
        Fields = PopulateLane();
    }

    public IList<IField> Fields { get; }

    protected abstract IList<IField> PopulateLane();
}