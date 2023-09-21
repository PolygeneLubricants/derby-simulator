using Derby.Engine.Race.Board;
using Derby.Engine.Race.Board.Lanes;
using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race;

public class RaceState
{
    public RaceState(GameBoard gameBoard, IEnumerable<OwnedHorse> horsesToRace)
    {
        Board = gameBoard;
        RegisteredHorses = Register(horsesToRace).ToList();
        _movableHorsesInRace = RegisteredHorses;
        NextHorseInTurn = 0;
        CurrentTurn = 0;
    }

    public GameBoard Board { get; }

    public required ChanceDeck ChanceDeck { get; init; }

    public required GallopDeck GallopDeck { get; init; }

    public IList<HorseInRace> RegisteredHorses { get; }

    private IList<HorseInRace> _movableHorsesInRace;

    public int NextHorseInTurn { get; private set; }
    public int CurrentTurn { get; private set; }

    public IList<HorseInRace> GetScore()
    {
        var horsesWhoHasNotCrashed = _movableHorsesInRace
            .Where(horse => horse.Modifiers.Select(modifier => modifier.Apply(horse, this)).All(resolution => !resolution.CountAsLast));

        var horsesWhoHasCrashed = _movableHorsesInRace
            .Where(horse => horse.Modifiers.Select(modifier => modifier.Apply(horse, this)).Any(resolution => resolution.CountAsLast)).Reverse();

        var unmodifiedScore = horsesWhoHasNotCrashed.OrderByDescending(horse => horse.GetLaneTiebreaker()).ToList();
        return unmodifiedScore.Concat(horsesWhoHasCrashed).Where(horse => !horse.Eliminated).ToList();
    }

    public HorseInRace GetLastHorse()
    {
        return _movableHorsesInRace.Reverse().OrderBy(horse => horse.GetLaneTiebreaker()).First();
    }

    public HorseInRace GetLeaderHorse()
    {
        return _movableHorsesInRace.OrderByDescending(horse => horse.GetLaneTiebreaker()).First();
    }

    public HorseInRace? GetHorseBehind(HorseInRace horseToFindBehind)
    {
        var horsesBySlowest          = _movableHorsesInRace.Reverse().OrderBy(horse => horse.GetLaneTiebreaker()).ToList();
        var indexOfHorseToFindBehind = horsesBySlowest.IndexOf(horseToFindBehind);
        if (indexOfHorseToFindBehind == 0)
        {
            return null;
        }
        else
        {
            return horsesBySlowest[indexOfHorseToFindBehind - 1];
        }
    }

    public HorseInRace? GetNextHorseInRace()
    {
        if (!_movableHorsesInRace.Any())
        {
            return null;
        }

        return _movableHorsesInRace[NextHorseInTurn];
    }

    public void IncrementTurnIfApplicable()
    {
        if (ShouldIncrementTurn())
        {
            CurrentTurn++;
        }
    }

    public void IncrementNextInTurnIfApplicable()
    {
        var previousCount = _movableHorsesInRace.Count;
        _movableHorsesInRace = _movableHorsesInRace.Where(horse => !horse.Eliminated).ToList();
        if (_movableHorsesInRace.Count == 0)
        {
            NextHorseInTurn = 0;
        }
        else if (previousCount > _movableHorsesInRace.Count && NextHorseInTurn != previousCount - 1)
        {
            // Horse has been eliminated, and counter should not be incremented
            // unless it is last.
        }
        else
        {
            NextHorseInTurn++;
            NextHorseInTurn %= _movableHorsesInRace.Count;
        }
    }

    private bool ShouldIncrementTurn()
    {
        return NextHorseInTurn >= _movableHorsesInRace.Count - 1;
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
            2 => Board.Lanes.Lane2Years,
            3 => Board.Lanes.Lane3Years,
            4 => Board.Lanes.Lane4Years,
            5 => Board.Lanes.Lane5Years,
            _ => throw new ArgumentException($"Horse age outside registered lanes. Age: '{horse.Years}'.")
        };
    }
}