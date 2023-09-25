using Derby.Engine.Race.Board.Lanes.PredefinedLanes;

namespace Derby.Engine.Race.Board;

/// <summary>
///     The derby game board.
/// </summary>
public class GameBoard
{
    /// <summary>
    ///     The lanes of the game board.
    /// </summary>
    public required LaneCollection Lanes { get; set; }

    /// <summary>
    ///     Constructs the pre-defined Derby board game board.
    /// </summary>
    /// <returns></returns>
    public static GameBoard DefaultBoard()
    {
        return new GameBoard
        {
            Lanes = new LaneCollection
            {
                Lane2Years = new Lane2Years(),
                Lane3Years = new Lane3Years(),
                Lane4Years = new Lane4Years(),
                Lane5Years = new Lane5Years()
            }
        };
    }
}