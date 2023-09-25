namespace Derby.Engine.Race.Board.Lanes;

/// <summary>
///     Determines how the closest horse is found.
/// </summary>
public enum SeekerStrategy
{
    /// <summary>
    ///     Closest horse is always the horse behind the current horse.
    /// </summary>
    Before,

    /// <summary>
    ///     Closest horse is either the horse in front or behind the current horse, depending on the absolute tie breaker
    ///     distance.
    /// </summary>
    Closest
}