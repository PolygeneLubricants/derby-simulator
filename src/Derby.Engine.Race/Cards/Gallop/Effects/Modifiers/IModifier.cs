using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

/// <summary>
///     Modifier-entity, which represents a permanent or prolonged effect on a horse, that can apply for multiple turns.
/// </summary>
public interface IModifier
{
    /// <summary>
    ///     Initialize the modifier for a given instance.
    ///     This will be initialized whenever a horse picks up an effect that applies a modifier.
    /// </summary>
    void Initialize(HorseInRace horseWithModifier, RaceState state);

    /// <summary>
    ///     Applies the modifier to the <paramref name="horseWithModifier" /> for the current <paramref name="state" />.
    /// </summary>
    ModifierResolution Apply(HorseInRace horseWithModifier, RaceState state);
}