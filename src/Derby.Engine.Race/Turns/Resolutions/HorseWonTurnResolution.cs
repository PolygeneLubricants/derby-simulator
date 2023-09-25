using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Turns.Resolutions;

/// <summary>
///     Turn resolution which indicates that a horse has won the race.
/// </summary>
public class HorseWonTurnResolution : ITurnResolution
{
    public HorseWonTurnResolution(IList<HorseInRace> score)
    {
        Score = score;
    }

    /// <summary>
    ///     Final score of the race, at the time of this resolution.
    /// </summary>
    public IList<HorseInRace> Score { get; init; }
}