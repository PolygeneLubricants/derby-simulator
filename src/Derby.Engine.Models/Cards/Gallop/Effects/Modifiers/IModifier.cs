namespace Derby.Engine.Models.Cards.Gallop.Effects.Modifiers;

public interface IModifier
{
    void Initialize(HorseInRace horseWithModifier, RaceState state);

    ModifierResolution Apply(HorseInRace horseWithModifier, RaceState state);
}