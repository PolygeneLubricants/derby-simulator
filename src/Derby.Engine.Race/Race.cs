using Derby.Engine.Race.Board;
using Derby.Engine.Race.Board.Lanes;
using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Horses;
using Derby.Engine.Race.Turns;
using Derby.Engine.Race.Turns.Resolutions;

namespace Derby.Engine.Race;

public class Race
{
    public Race(IEnumerable<OwnedHorse> horsesToRace)
    {
        State = new RaceState
        {
            Board = new GameBoard(),
            GallopDeck = GallopDeck.DefaultDeck(),
            ChanceDeck = ChanceDeck.DefaultDeck(),
            HorsesInRace = Register(horsesToRace).ToList()
        };

        _turnResolver = new TurnResolver();
    }

    public RaceState State { get; init; }

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

    private IEnumerable<HorseInRace> Register(IEnumerable<OwnedHorse> horsesToRace)
    {
        return horsesToRace.Select(Register);
    }

    private HorseInRace Register(OwnedHorse ownedHorse)
    {
        var horseInRace = new HorseInRace { OwnedHorse = ownedHorse, Lane = MapLane(ownedHorse.Horse) };
        return horseInRace;
    }

    private ILane MapLane(Horse horse)
    {
        return horse.Years switch
        {
            2 => State.Board.Lanes.Lane2Years,
            3 => State.Board.Lanes.Lane3Years,
            4 => State.Board.Lanes.Lane4Years,
            5 => State.Board.Lanes.Lane5Years,
            _ => throw new ArgumentException($"Horse age outside registered lanes. Age: '{horse.Years}'.")
        };
    }
}