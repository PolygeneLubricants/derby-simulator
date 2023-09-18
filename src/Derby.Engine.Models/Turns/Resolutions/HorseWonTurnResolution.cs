namespace Derby.Engine.Models.Turns.Resolutions;

public class HorseWonTurnResolution : ITurnResolution
{
    public HorseWonTurnResolution(IList<HorseInRace> score)
    {
        Score = score;
    }

    public IList<HorseInRace> Score { get; init; }
}