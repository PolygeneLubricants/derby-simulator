using Derby.Engine.Models.Board;
using Derby.Engine.Models.Board.Lanes;
using Derby.Engine.Models.Cards.Chance;
using Derby.Engine.Models.Cards.Gallop;
using Derby.Engine.Models.Horses;
using Derby.Engine.Models.Turns;

namespace Derby.Engine.Models;

public class Race
{
    public Race(IEnumerable<OwnedHorse> horsesToRace)
    {
        State = new RaceState
        {
            Board = new GameBoard(),
            GallopDeck = new GallopDeck(),
            ChanceDeck = new ChanceDeck()
        };

        _turnResolver = new TurnResolver();
        HorsesInRace = Register(horsesToRace);
    }

    public RaceState State { get; init; }

    public IList<HorseInRace> HorsesInRace { get; init; }

    private TurnResolver _turnResolver;

    public TurnResolution ResolveTurn()
    {
        var horseToPlay = HorsesInRace[State.NextInTurn];
        return _turnResolver.ResolveTurn(horseToPlay, State);
    }

    private IList<HorseInRace> Register(IEnumerable<OwnedHorse> horsesToRace)
    {
        foreach (var ownedHorse in horsesToRace)
        {
            Register(ownedHorse);
        }
    }

    private HorseInRace Register(OwnedHorse ownedHorse)
    {
        var horseInRace = new HorseInRace { OwnedHorse = ownedHorse };
        horseInRace.Lane = MapLane(ownedHorse.Horse);
        horseInRace.Lane.Register(ownedHorse);
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