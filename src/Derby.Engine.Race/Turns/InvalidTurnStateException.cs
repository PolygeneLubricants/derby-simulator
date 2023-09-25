using Derby.Engine.Race.Board.Lanes.Fields;

namespace Derby.Engine.Race.Turns;

/// <summary>
///     The turn resolver has landed on a state which should not be possible within the current turn.
/// </summary>
public class InvalidTurnStateException : InvalidOperationException
{
    public InvalidTurnStateException(IField fieldHorseLandedOn) : base($"Invalid turn state for field {fieldHorseLandedOn.GetType().Name}")
    {
    }
}