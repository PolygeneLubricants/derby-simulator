using Derby.Engine.Models.Board.Lanes;

namespace Derby.Engine.Models;

public class HorseInRace
{
    public OwnedHorse OwnedHorse { get; init; }

    public ILane Lane { get; set; }
}