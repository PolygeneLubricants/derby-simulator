namespace Derby.Engine.Race;

/// <summary>
///     The Derby game has five stables, each owned by a player.
/// </summary>
public class Stable
{
    /// <summary>
    ///     The code of this stable.
    /// </summary>
    public required StableCode Code { get; init; }
}