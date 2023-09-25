namespace Derby.Engine.Race;

/// <summary>
///     A player is an owner of one or more horses through one stable.
/// </summary>
public class Player
{
    /// <summary>
    ///     The stable which the player owns.
    /// </summary>
    public required Stable Stable { get; init; }
}