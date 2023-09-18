using Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

namespace Derby.Engine.Race.Cards.Gallop.Effects;

// "(Gælder ikke for hest redet af stjernejockey) Lad hesten stå til alle er passeret, eller sidste hest er på linje med Deres. Hvis nogen går i mål regnes Deres hest for at være sidst. Gem kortert til det ikke gælder længere."
public class CrashSpecialEffect : IGallopCardEffect
{
    public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        var awaitModifier = new AwaitModifier(AwaitType.All);
        awaitModifier.Initialize(horseToPlay, state);
        var awaitModifierResolution = awaitModifier.Apply(horseToPlay, state);

        if (awaitModifierResolution.IsApplicable)
        {
            horseToPlay.Modifiers.Add(awaitModifier);
        }

        var isLastModifier = new IsLastModifier();
        isLastModifier.Initialize(horseToPlay, state);
        var isLastModifierResolution = isLastModifier.Apply(horseToPlay, state);
        if (isLastModifierResolution.IsApplicable)
        {
            horseToPlay.Modifiers.Add(isLastModifier);
        }

        return new GallopCardResolution();
    }
}