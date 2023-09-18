using Derby.Engine.Models.Board;
using Derby.Engine.Models.Cards.Chance;
using Derby.Engine.Models.Cards.Gallop;

namespace Derby.Engine.Models;

public class RaceState
{
    public RaceState()
    {
        NextHorseInTurn = 0;
        CurrentTurn = 0;
    }

    public required GameBoard Board { get; init; }

    public required ChanceDeck ChanceDeck { get; init; }

    public required GallopDeck GallopDeck { get; init; }

    public required IList<HorseInRace> HorsesInRace { get; init; }

    public int NextHorseInTurn { get; private set; }
    public int CurrentTurn { get; private set; }

    public IList<HorseInRace> GetScore()
    {
        return HorsesInRace.OrderByDescending(horse => horse.Location).ToList();
    }

    public HorseInRace GetLastHorse()
    {
        return HorsesInRace.Reverse().OrderBy(horse => horse.Location).First();
    }

    public HorseInRace GetLeaderHorse()
    {
        return HorsesInRace.OrderByDescending(horse => horse.Location).First();
    }

    public HorseInRace? GetHorseBehind(HorseInRace horseToFindBehind)
    {
        var horsesBySlowest = HorsesInRace.OrderBy(horse => horse.Location).ToList();
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
        var horsesInRace = HorsesInRace.Where(h => !h.Eliminated).ToList();
        if (!horsesInRace.Any())
        {
            return null;
        }

        return horsesInRace[NextHorseInTurn];
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
        NextHorseInTurn %= GetMovableHorsesInRace();
    }

    private bool ShouldIncrementTurn()
    {
        return NextHorseInTurn >= GetMovableHorsesInRace() - 1;
    }

    private int GetMovableHorsesInRace()
    {
        return HorsesInRace.Count(h => !h.Eliminated);
    }
}