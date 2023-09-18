namespace Derby.Engine.Models.Turns.Resolutions;

public class HorseEliminatedTurnResolution : ITurnResolution
{
    public HorseEliminatedTurnResolution(HorseInRace eliminatedHorse)
    {
        EliminatedHorse = eliminatedHorse;
    }

    public HorseInRace EliminatedHorse { get; init; }
}