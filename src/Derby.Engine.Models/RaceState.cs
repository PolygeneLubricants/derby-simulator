using Derby.Engine.Models.Board;
using Derby.Engine.Models.Cards.Chance;
using Derby.Engine.Models.Cards.Gallop;

namespace Derby.Engine.Models;

public class RaceState
{
    public RaceState()
    {
        NextInTurn = 0;
        CurrentTurnNumber = 0;
    }

    public required GameBoard Board { get; init; }

    public required ChanceDeck ChanceDeck { get; init; }

    public required GallopDeck GallopDeck { get; init; }

    public int NextInTurn { get; init; }
    public int CurrentTurnNumber { get; init; }
}