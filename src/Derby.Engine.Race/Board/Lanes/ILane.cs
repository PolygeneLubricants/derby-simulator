using Derby.Engine.Race.Board.Lanes.Fields;

namespace Derby.Engine.Race.Board.Lanes;

public interface ILane
{
    IList<IField> Fields { get; }
}