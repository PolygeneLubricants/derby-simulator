using Derby.Engine.Models.Board.Lanes.Fields;

namespace Derby.Engine.Models.Cards.Gallop;

public class GallopCardResolution
{
    public IField? NewField { get; set; }

    public bool EndTurn { get; set; }

    public bool HorseDisqualified { get; set; }
}