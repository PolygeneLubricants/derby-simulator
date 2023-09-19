using System.Collections.Immutable;
using Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

namespace Derby.Engine.Race.Horses;

public class Horse
{
    public required IList<int> Moves { get; init; }

    public required string Name { get; init; }

    public required int Years { get; init; }

    public required Color Color { get; init; }

    public int GetMoves(int turn)
    {
        return GetMoves(turn, ImmutableList<ModifierResolution>.Empty);
    }

    public int GetMoves(int turn, IList<ModifierResolution> modifierResolution)
    {
        var nextMoves = Moves[turn % Moves.Count]; // Wrap around if turn is more than max moves.
        if (!modifierResolution.Any())
        {
            return nextMoves;
        }

        var applicableModifiers = modifierResolution.Where(modifier => modifier.IsApplicable);
        var overwrittenMoves = applicableModifiers.FirstOrDefault(modifier => modifier.Moves.HasValue)?.Moves.Value;
        if (overwrittenMoves != null)
        {
            nextMoves = overwrittenMoves.Value;
        }

        var maxMoves = applicableModifiers.FirstOrDefault(modifier => modifier.MaxMoves.HasValue)?.MaxMoves.Value;
        if (maxMoves != null && nextMoves > maxMoves.Value)
        {
            nextMoves = maxMoves.Value;
        }

        return nextMoves;
    }
}