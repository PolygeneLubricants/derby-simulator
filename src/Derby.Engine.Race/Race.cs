using Derby.Engine.Race.Board;
using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Horses;
using Derby.Engine.Race.Turns;
using Derby.Engine.Race.Turns.Resolutions;

namespace Derby.Engine.Race;

/// <summary>
///     Entity encapsulating an entire Derby race.
///     This entity is the entry point to construct, run and resolve a race.
/// </summary>
public class Race
{
    /// <summary>
    ///     Turn resolver to resolve each race turn.
    /// </summary>
    public readonly TurnResolver TurnResolver;

    public Race()
    {
        TurnResolver = new TurnResolver();
    }

    /// <summary>
    ///     Mutable state of this race.
    /// </summary>
    public required RaceState State { get; init; }

    /// <summary>
    ///     The final resolution of the race, after it has been concluded.
    ///     Will be null until <see cref="GameEnded" /> is true.
    /// </summary>
    public ITurnResolution? FinalResolution { get; private set; }

    /// <summary>
    ///     Indicates whether this race has been run to completion.
    /// </summary>
    public bool GameEnded { get; private set; }

    /// <summary>
    ///     Event listener which fires when a horse starts its turn.
    /// </summary>
    public event Action<HorseInRace> TurnStarted = horseInRace => { };

    /// <summary>
    ///     Returns the default Derby race.
    ///     Default is the configuration which the official board game has.
    /// </summary>
    public static Race GetDefault(IEnumerable<OwnedHorse> horsesToRace)
    {
        var race = new Race
        {
            State = new RaceState(GameBoard.DefaultBoard(), horsesToRace)
            {
                GallopDeck = GallopDeck.DefaultDeck(),
                ChanceDeck = ChanceDeck.DefaultDeck()
            }
        };

        return race;
    }

    /// <summary>
    ///     Resolves 1 turn for 1 horse.
    /// </summary>
    public ITurnResolution ResolveTurn()
    {
        var horseToPlay = State.GetNextHorseInRace();
        if (TurnStarted != null && horseToPlay != null)
        {
            TurnStarted.Invoke(horseToPlay);
        }

        ITurnResolution resolution;
        if (horseToPlay == null)
        {
            resolution = new DrawTurnResolution();
        }
        else
        {
            resolution = TurnResolver.ResolveTurn(horseToPlay, State);
        }

        CleanupTurn(State, horseToPlay);

        if (resolution is DrawTurnResolution or HorseWonTurnResolution)
        {
            GameEnded = true;
            FinalResolution = resolution;
        }

        return resolution;
    }

    /// <summary>
    ///     Cleans up the race state after a turn has been resolved.
    /// </summary>
    private void CleanupTurn(RaceState state, HorseInRace? horseToPlay)
    {
        state.IncrementTurnIfApplicable();
        state.IncrementNextInTurnIfApplicable();
        horseToPlay?.CleanupTurn();
    }
}