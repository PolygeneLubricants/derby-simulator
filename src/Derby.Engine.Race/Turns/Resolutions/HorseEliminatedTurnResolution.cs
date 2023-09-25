using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Turns.Resolutions;

/// <summary>
///     Resolution indicating that a horse, eliminatedHorse, has been eliminated.
/// </summary>
public class HorseEliminatedTurnResolution : ITurnResolution
{
    public HorseEliminatedTurnResolution(HorseInRace eliminatedHorse)
    {
        EliminatedHorse = eliminatedHorse;
    }

    /// <summary>
    ///     Horse that has been eliminated as part of this turn resolution.
    /// </summary>
    public HorseInRace EliminatedHorse { get; init; }
}