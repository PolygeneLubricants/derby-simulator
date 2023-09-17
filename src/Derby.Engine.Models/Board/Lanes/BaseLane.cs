using Derby.Engine.Models.Board.Lanes.Fields;

namespace Derby.Engine.Models.Board.Lanes;

public abstract class BaseLane
{
    protected BaseLane()
    {
        Fields = PopulateLane();
    }

    public IList<IField> Fields { get; set; }

    protected abstract IList<IField> PopulateLane();
}