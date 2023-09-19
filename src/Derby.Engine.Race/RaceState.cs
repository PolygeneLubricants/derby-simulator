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
        HorsesInRace = Register(horsesToRace).ToList();
        NextHorseInTurn = 0;
        CurrentTurn = 0;
    }

    public GameBoard Board { get; }

    public required ChanceDeck ChanceDeck { get; init; }

    public required GallopDeck GallopDeck { get; init; }

    public IList<HorseInRace> HorsesInRace { get; }

    public int NextHorseInTurn { get; private set; }
    public int CurrentTurn { get; private set; }

    public IList<HorseInRace> GetScore()
    {
        return HorsesInRace.OrderByDescending(horse => horse.GetLaneTiebreaker()).ToList();
    }

    public HorseInRace GetLastHorse()
    {
        return HorsesInRace.Reverse().OrderBy(horse => horse.GetLaneTiebreaker()).First();
    }

    public HorseInRace GetLeaderHorse()
    {
        return HorsesInRace.OrderByDescending(horse => horse.GetLaneTiebreaker()).First();
    }

    public HorseInRace? GetHorseBehind(HorseInRace horseToFindBehind)
    {
        var horsesBySlowest = HorsesInRace.OrderBy(horse => horse.GetLaneTiebreaker()).ToList();
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
        var horsesStillInRace = GetMovableHorsesInRace().ToList();
        if (!horsesStillInRace.Any())
        {
            return null;
        }

        return horsesStillInRace[NextHorseInTurn];
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
        NextHorseInTurn++;
        NextHorseInTurn %= GetMovableHorsesInRace().Count();
    }

    private bool ShouldIncrementTurn()
    {
        return NextHorseInTurn >= GetMovableHorsesInRace().Count() - 1;
    }

    private IEnumerable<HorseInRace> GetMovableHorsesInRace()
    {
        return HorsesInRace.Where(h => !h.Eliminated);
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