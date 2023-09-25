namespace Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

/// <summary>
///     Modifier resolution, when a modifier is resolved.
/// </summary>
public class ModifierResolution
{
    /// <summary>
    ///     Indicates whether this modifier is applicable. If a modifier is not applicable, it is removed from a
    ///     <see cref="HorseInRace" />.
    /// </summary>
    public required bool IsApplicable { get; set; }

    /// <summary>
    ///     Indicates that the horse's turn ends immediately.
    /// </summary>
    public bool EndTurn { get; set; }

    /// <summary>
    ///     Indicates that the modifier puts a max value to the moves this horse can perform.
    /// </summary>
    public int? MaxMoves { get; set; }

    /// <summary>
    ///     Indicates the number of moves this modifier adds to the horse's turn.
    /// </summary>
    public int? Moves { get; set; }

    /// <summary>
    ///     Indicates whether this horse should skip drawing Gallop cards.
    /// </summary>
    public bool SkipGallopCards { get; set; }

    /// <summary>
    ///     Indicates that the horse should count as last, if the race ends this turn.
    /// </summary>
    public bool CountAsLast { get; set; }
}