using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

/// <summary>
///     Modifier that skips the horse's next turn.
/// </summary>
public class SkipTurnModifier : IModifier
{
    private int _applicableTurn;

    public void Initialize(HorseInRace horseWithModifier, RaceState state)
    {
        _applicableTurn = state.CurrentTurn;
    }

    public ModifierResolution Apply(HorseInRace horseWithModifier, RaceState state)
    {
        if (state.CurrentTurn > _applicableTurn + 1)
        {
            return new ModifierResolution { IsApplicable = false };
        }

        return new ModifierResolution { IsApplicable = true, EndTurn = true };
    }
}