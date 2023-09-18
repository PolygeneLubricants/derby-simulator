namespace Derby.Engine.Models.Cards.Gallop.Effects.Modifiers;

public class MaxMoveModifier : IModifier
{
    private readonly int _maxMoves;
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