namespace Derby.Engine.Models.Cards.Gallop.Effects.Modifiers;

public class SteadyPaceModifier : IModifier
{
    private readonly int _pace;

    public SteadyPaceModifier(int pace)
    {
        _pace = pace;
    }

    public void Initialize(HorseInRace horseWithModifier, RaceState state)
    {
    }

    public ModifierResolution Apply(HorseInRace horseWithModifier, RaceState state)
    {
        return new ModifierResolution
        {
            IsApplicable = true, 
            Moves = _pace, 
            SkipGallopCards = true
        };
    }
}