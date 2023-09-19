using Derby.Engine.Race.Board.Lanes.Fields;

namespace Derby.Engine.Race.Turns;

public class InvalidTurnStateException : InvalidOperationException
{
    public InvalidTurnStateException(IField fieldHorseLandedOn) : base($"Invalid turn state for field {fieldHorseLandedOn.GetType().Name}")
    {
    }
}