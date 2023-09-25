using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

/// <summary>
///     Implements the steady-pace modifier.
///     From here-on the horse always moves 4 fields, and no longer draws *any* Gallop cards.
/// </summary>
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