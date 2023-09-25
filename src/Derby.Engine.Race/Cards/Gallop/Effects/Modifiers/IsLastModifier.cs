using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

/// <summary>
///     Special modifier that implements the is last part of the crash card with the following text:
///     (Gælder ikke for hest redet af stjernejockey) Lad hesten stå til alle er passeret, eller sidste hest er på linje
///     med Deres. Hvis nogen går i mål regnes Deres hest for at være sidst. Gem kortert til det ikke gælder længere.
///     Specifically:
///     Hvis nogen går i mål regnes Deres hest for at være sidst.
/// </summary>
public class IsLastModifier : IModifier
{
    private int _applicableTurn;

    public void Initialize(HorseInRace horseWithModifier, RaceState state)
    {
        _applicableTurn = state.CurrentTurn;
    }

    public ModifierResolution Apply(HorseInRace horseWithModifier, RaceState state)
    {
        if (_applicableTurn != state.CurrentTurn)
        {
            return new ModifierResolution { IsApplicable = false };
        }

        return new ModifierResolution { IsApplicable = true, CountAsLast = true };
    }
}