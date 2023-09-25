using Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;
using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop.Effects;

public class ModifierEffect : IGallopCardEffect
{
    private readonly Func<IModifier> _modifierDelegate;

    public ModifierEffect(Func<IModifier> modifierDelegate)
    {
        _modifierDelegate = modifierDelegate;
    }

    public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        // Attempt to apply modifier initially, to see if effect applies at all.
        var modifier = _modifierDelegate.Invoke();
        modifier.Initialize(horseToPlay, state);
        var resolution = modifier.Apply(horseToPlay, state);
     
        if (resolution.IsApplicable)
        {
            horseToPlay.Modifiers.Add(modifier);
        }

        return new GallopCardResolution();
    }
}