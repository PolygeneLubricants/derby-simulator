using System.Collections.Immutable;
using Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

namespace Derby.Engine.Race.Horses;

/// <summary>
///     Horse entity, that represents a static purchasable horse in Derby.
/// </summary>
public class Horse
{
    /// <summary>
    ///     The move-set that this horse will follow at each turn.
    /// </summary>
    public required IList<int> Moves { get; init; }

    /// <summary>
    ///     The name of the horse.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    ///     The horse's age in years.
    /// </summary>
    public required int Years { get; init; }

    /// <summary>
    ///     The horse's color.
    /// </summary>
    public required Color Color { get; init; }

    /// <summary>
    ///     Gets the moves that this horse would move naturally in turn <paramref name="turn" />,
    ///     ignoring any modifiers.
    /// </summary>
    public int GetMoves(int turn)
    {
        return GetMoves(turn, ImmutableList<ModifierResolution>.Empty);
    }

    /// <summary>
    ///     Gets the moves that this horse would move naturally in turn <paramref name="turn" />,
    ///     while considered any applicable modifiers.
    /// </summary>
    public int GetMoves(int turn, IList<ModifierResolution> modifierResolution)
    {
        var nextMoves = Moves[turn % Moves.Count]; // Wrap around if turn is more than max moves.
        if (!modifierResolution.Any())
        {
            return nextMoves;
        }

        var applicableModifiers = modifierResolution.Where(modifier => modifier.IsApplicable);
        var overwrittenMoves    = applicableModifiers.FirstOrDefault(modifier => modifier.Moves.HasValue)?.Moves.Value;
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