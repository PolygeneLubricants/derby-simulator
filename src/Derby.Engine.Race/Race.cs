using Derby.Engine.Race.Board;
using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Turns;
using Derby.Engine.Race.Turns.Resolutions;

namespace Derby.Engine.Race;

public class Race
{
    public static Race GetDefault(IEnumerable<OwnedHorse> horsesToRace)
    {
        var race = new Race()
        {
            State = new RaceState(GameBoard.DefaultBoard(), horsesToRace)
            {
                GallopDeck = GallopDeck.DefaultDeck(),
                ChanceDeck = ChanceDeck.DefaultDeck()
            }
        };

        return race;
    }

    public Race()
    {
        _turnResolver = new TurnResolver();
    }

    public required RaceState State { get; init; }

    private readonly TurnResolver _turnResolver;

    public ITurnResolution ResolveTurn()
    {
        var horseToPlay = State.GetNextHorseInRace();
        if (horseToPlay == null)
        {
            return new DrawTurnResolution();
        }

        return _turnResolver.ResolveTurn(horseToPlay, State);
    }
}