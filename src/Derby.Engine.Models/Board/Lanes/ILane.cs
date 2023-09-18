using Derby.Engine.Models.Board.Lanes.Fields;

namespace Derby.Engine.Models.Board.Lanes;

public interface ILane
{
    IList<IField> Fields { get; }
}