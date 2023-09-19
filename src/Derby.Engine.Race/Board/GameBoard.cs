using Derby.Engine.Race.Board.Lanes;

namespace Derby.Engine.Race.Board
{
    public class GameBoard
    {
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

        public required LaneCollection Lanes { get; set; }
    }
}
