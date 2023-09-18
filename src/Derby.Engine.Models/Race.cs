using Derby.Engine.Models.Board;
using Derby.Engine.Models.Board.Lanes;
using Derby.Engine.Models.Cards.Chance;
using Derby.Engine.Models.Cards.Gallop;
using Derby.Engine.Models.Horses;
using Derby.Engine.Models.Turns;
using Derby.Engine.Models.Turns.Resolutions;

namespace Derby.Engine.Models;

public class Race
{
    public Race(IEnumerable<OwnedHorse> horsesToRace)
    {
        State = new RaceState
        {
            Board = new GameBoard(),
            GallopDeck = new GallopDeck(),
            ChanceDeck = new ChanceDeck(),
            HorsesInRace = Register(horsesToRace).ToList()
    };

        _turnResolver = new TurnResolver();
    }

    public RaceState State { get; init; }

    private readonly TurnResolver _turnResolver;

    public ITurnResolution ResolveTurn()
    {
        var horseToPlay = State.GetNextHorseInRace();
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