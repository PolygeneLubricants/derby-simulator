using Derby.Engine.Race.Board.Lanes.Fields;

namespace Derby.Engine.Race.Board.Lanes;

/// <summary>
///     A lane is a track that horses run on.
/// </summary>
public interface ILane
{
    /// <summary>
    ///     The ordered list of fields which this lane contains.
    /// </summary>
    IList<IField> Fields { get; }

    /// <summary>
    ///     Gets the tie-breaker value of the field which the horse is currently standing on.
    ///     Due to lane curvature, the tie-breaker value is not the same as the field index, but rather the distance from the
    ///     goal,
    ///     relative to other lanes.
    /// </summary>
    int GetTiebreaker(int horseLocation);

    /// <summary>
    ///     Gets the horse closest to the current horse, based on the seeker strategy.
    /// </summary>
    int GetClosestLocation(int tieBreakToSeek, SeekerStrategy strategy);
}