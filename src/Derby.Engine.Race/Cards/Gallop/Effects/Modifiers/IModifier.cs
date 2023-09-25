using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

public interface IModifier
{
    void Initialize(HorseInRace horseWithModifier, RaceState state);

    ModifierResolution Apply(HorseInRace horseWithModifier, RaceState state);
}