using Derby.Engine.Race.Board;
using Derby.Engine.Race.Board.Lanes;
using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race;

/// <summary>
///     Tracks the mutable state of the current Derby race.
/// </summary>
public class RaceState
{
    /// <summary>
    ///     List of horses which are still in the game. I.e. horses that have not been eliminated.
    /// </summary>
    private IList<HorseInRace> _movableHorsesInRace;

    public RaceState(GameBoard gameBoard, IEnumerable<OwnedHorse> horsesToRace)
    {
        Board = gameBoard;
        RegisteredHorses = Register(horsesToRace).ToList();
        _movableHorsesInRace = RegisteredHorses;
        NextHorseInTurn = 0;
        CurrentTurn = 0;
    }

    /// <summary>
    ///     The game board used the current state.
    /// </summary>
    public GameBoard Board { get; }

    /// <summary>
    ///     The chance deck used for the current state.
    /// </summary>
    public required ChanceDeck ChanceDeck { get; init; }

    /// <summary>
    ///     The gallop deck used for the current state.
    /// </summary>
    public required GallopDeck GallopDeck { get; init; }

    /// <summary>
    ///     The list of horses which were initially registered for this race. This includes horses which have been eliminated.
    /// </summary>
    public IList<HorseInRace> RegisteredHorses { get; }

    /// <summary>
    ///     Array index of the next horse to take a turn.
    ///     This value will remain the same until the horse's turn has ended.
    /// </summary>
    /// <example cref="CurrentTurn"></example>
    public int NextHorseInTurn { get; private set; }

    /// <summary>
    ///     Array index of the current turn.
    ///     Each turn is separated into "sub-turn" where each horse moves.
    /// </summary>
    /// <example>
    ///     If a race has three horses, the <see cref="NextHorseInTurn" /> and <see cref="CurrentTurn" /> will increment
    ///     as such:
    ///     Turn 0: Horse 0
    ///     Turn 0: Horse 1
    ///     Turn 0: Horse 2
    ///     Turn 1: Horse 0
    ///     ...
    /// </example>
    public int CurrentTurn { get; private set; }

    /// <summary>
    ///     Get the current score of this race.
    ///     The current score will show the leader as first, and the last horse as last.
    ///     Any horses that have been eliminated are always last, regardless of their position on the board.
    ///     If a horse has the crashed modifier, it will be placed last, but ahead of any eliminated horses.
    /// </summary>
    /// <returns></returns>
    public IList<HorseInRace> GetScore()
    {
        var horsesWhoHasNotCrashed = _movableHorsesInRace
            .Where(horse => horse.Modifiers.Select(modifier => modifier.Apply(horse, this)).All(resolution => !resolution.CountAsLast));

        var horsesWhoHasCrashed = _movableHorsesInRace
            .Where(horse => horse.Modifiers.Select(modifier => modifier.Apply(horse, this)).Any(resolution => resolution.CountAsLast)).Reverse();

        var eliminatedHorses = RegisteredHorses.Where(horse => horse.Eliminated).ToList();

        var unmodifiedScore = horsesWhoHasNotCrashed.OrderByDescending(horse => horse.GetLaneTiebreaker()).ToList();
        return unmodifiedScore.Concat(horsesWhoHasCrashed).Concat(eliminatedHorses).ToList();
    }

    /// <summary>
    ///     Gets the current last horse on the board across all lanes.
    ///     If horses tie on the same square, the horse which is later in turn order is considered behind.
    /// </summary>
    public HorseInRace GetLastHorse()
    {
        return _movableHorsesInRace.Reverse().OrderBy(horse => horse.GetLaneTiebreaker()).First();
    }

    /// <summary>
    ///     Gets the first horses on the board across all lanes.
    ///     If horses tie on the same square, the horse which is earlier in turn order is considered ahead.
    /// </summary>
    public HorseInRace GetLeaderHorse()
    {
        return _movableHorsesInRace.OrderByDescending(horse => horse.GetLaneTiebreaker()).First();
    }

    /// <summary>
    ///     Gets the horse that is immediately behind the current horse on the board.
    ///     If horses tie on the same square, the horse which is later in turn order is considered behind.
    /// </summary>
    public HorseInRace? GetHorseBehind(HorseInRace horseToFindBehind)
    {
        var horsesBySlowest          = _movableHorsesInRace.Reverse().OrderBy(horse => horse.GetLaneTiebreaker()).ToList();
        var indexOfHorseToFindBehind = horsesBySlowest.IndexOf(horseToFindBehind);
        if (indexOfHorseToFindBehind == 0)
        {
            return null;
        }

        return horsesBySlowest[indexOfHorseToFindBehind - 1];
    }

    /// <summary>
    ///     Gets the horse which is the next to take turn, as determined by <see cref="NextHorseInTurn" />.
    ///     Note: This value is pure and only increments at the end of the horse's turn.
    /// </summary>
    public HorseInRace? GetNextHorseInRace()
    {
        if (!_movableHorsesInRace.Any())
        {
            return null;
        }

        return _movableHorsesInRace[NextHorseInTurn];
    }

    /// <summary>
    ///     Increments the current turn if the current horse is the last horse in turn order.
    /// </summary>
    public void IncrementTurnIfApplicable()
    {
        if (ShouldIncrementTurn())
        {
            CurrentTurn++;
        }
    }

    /// <summary>
    ///     Increments the next horse in turn order.
    ///     Note, if the current horse as just been eliminated, the counter will not be incremented, as the number of movable
    ///     horses are instead reduced.
    ///     Except if the horse is the last horse in turn order, in which case the counter will be incremented.
    /// </summary>
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

    /// <summary>
    ///     Determines whether the turn should be incremented, or remain the same.
    ///     The turn will remain the same until all horse have moved within this turn.
    /// </summary>
    private bool ShouldIncrementTurn()
    {
        return NextHorseInTurn >= _movableHorsesInRace.Count - 1;
    }

    /// <summary>
    ///     Registers a horse to this race.
    /// </summary>
    private IEnumerable<HorseInRace> Register(IEnumerable<OwnedHorse> horsesToRace)
    {
        return horsesToRace.Select(Register);
    }

    /// <summary>
    ///     Registers a horse to this race.
    /// </summary>
    private HorseInRace Register(OwnedHorse ownedHorse)
    {
        var horseInRace = new HorseInRace { OwnedHorse = ownedHorse, Lane = MapLane(ownedHorse.Horse) };
        return horseInRace;
    }

    /// <summary>
    ///     Maps a horse to the appropriate lane, based on the horse's age.
    /// </summary>
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