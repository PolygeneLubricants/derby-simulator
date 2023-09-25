using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

/// <summary>
///     Modifier that puts a limit to the number of moves a horse can make in a turn.
///     If the horse naturally moves 4 but has a max move modifier of 2, it will only move 2.
/// </summary>
public class MaxMoveModifier : IModifier
{
    /// <summary>
    ///     The max number of moves a hse can make in a turn.
    /// </summary>
    private readonly int _maxMoves;

    /// <summary>
    ///     The maximum turn in which this modifier is applicable.
    /// </summary>
    private int _applicableTurn;

    public MaxMoveModifier(int maxMoves)
    {
        _maxMoves = maxMoves;
    }

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

        return new ModifierResolution { IsApplicable = true, MaxMoves = _maxMoves };
    }
}