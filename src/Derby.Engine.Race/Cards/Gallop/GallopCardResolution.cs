using Derby.Engine.Race.Board.Lanes.Fields;

namespace Derby.Engine.Race.Cards.Gallop;

public class GallopCardResolution
{
    public IField? NewField { get; set; }

    public bool IsMoveResolution => NewField != null;

    public bool EndTurn { get; set; }

    public bool HorseDisqualified { get; set; }
}