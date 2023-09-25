using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop.Effects;

/// <summary>
///     Moves the horse a number of fields.
/// </summary>
public class MoveEffect : IGallopCardEffect
{
    private readonly int _moves;

    public MoveEffect(int moves)
    {
        _moves = moves;
    }

    public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        // Detect max moves
        var modifierResolutions = horseToPlay.Modifiers.Select(modifier => modifier.Apply(horseToPlay, state));
        var maxMoveModifier     = modifierResolutions.FirstOrDefault(resolution => resolution.IsApplicable && resolution.MaxMoves.HasValue);
        var moves               = _moves;

        if (maxMoveModifier != null && maxMoveModifier.MaxMoves.Value > moves)
        {
            moves = maxMoveModifier.MaxMoves.Value;
        }

        var field = horseToPlay.Move(moves, MoveType.CardEffect);
        return new GallopCardResolution
        {
            NewField = field
        };
    }
}