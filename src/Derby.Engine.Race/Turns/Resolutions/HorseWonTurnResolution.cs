using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Turns.Resolutions;

public class HorseWonTurnResolution : ITurnResolution
{
    public HorseWonTurnResolution(IList<HorseInRace> score)
    {
        Score = score;
    }

    public IList<HorseInRace> Score { get; init; }
}