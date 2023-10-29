using Derby.Engine.Race.Game.GlobalModifiers;

namespace Derby.Engine.Race.Game;

public class GameRace
{
    public GameRace(
        string name,
        string description,
        IGlobalModifier globalModifier,
        HorsesCriteria eligibleHorsesCriteria)
    {
        Name = name;
        Description = description;
        GlobalModifier = globalModifier;
        EligibleHorsesCriteria = eligibleHorsesCriteria;
    }

    public string Name { get; init; }

    public string Description { get; init; }

    public IGlobalModifier GlobalModifier { get; init; }

    public HorsesCriteria EligibleHorsesCriteria { get; init; }
}