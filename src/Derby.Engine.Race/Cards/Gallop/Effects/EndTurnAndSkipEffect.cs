using Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

namespace Derby.Engine.Race.Cards.Gallop.Effects;

public class EndTurnAndSkipEffect : IGallopCardEffect
{
    public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        var skipTurnModifier = new SkipTurnModifier();
        skipTurnModifier.Initialize(horseToPlay, state);
        var awaitModifierResolution = skipTurnModifier.Apply(horseToPlay, state);

        if (awaitModifierResolution.IsApplicable)
        {
            horseToPlay.Modifiers.Add(skipTurnModifier);
        }

        return new GallopCardResolution();
    }
}