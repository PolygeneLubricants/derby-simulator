using Derby.Engine.Race.Board;
using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Turns;
using Derby.Engine.Race.Turns.Resolutions;

namespace Derby.Engine.Race;

public class Race
{
    public event Action<ChanceCard>? ChanceCardDrawn;
    public event Action<GallopCard>? GallopCardDrawn;

    public static Race GetDefault(IEnumerable<OwnedHorse> horsesToRace)
    {
        Action<ChanceCard> chanceCardDrawn = delegate { };
        Action<GallopCard> gallopCardDrawn = delegate { };

        var race = new Race(chanceCardDrawn, gallopCardDrawn)
        {
            State = new RaceState(GameBoard.DefaultBoard(), horsesToRace)
            {
                GallopDeck = GallopDeck.DefaultDeck(gallopCardDrawn),
                ChanceDeck = ChanceDeck.DefaultDeck(chanceCardDrawn),
            },
            ChanceCardDrawn = chanceCardDrawn,
            GallopCardDrawn = gallopCardDrawn
        };

        return race;
    }

    public Race()
    {
        ChanceCardDrawn = null;
        GallopCardDrawn = null;
        _turnResolver = new TurnResolver();
    }

    public Race(
        Action<ChanceCard> chanceCardDrawn, 
        Action<GallopCard> gallopCardDrawn)
    {
        ChanceCardDrawn = chanceCardDrawn;
        GallopCardDrawn = gallopCardDrawn;
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

        var resolution = _turnResolver.ResolveTurn(horseToPlay, State);
        CleanupTurn(State, horseToPlay);
        return resolution;
    }

    private void CleanupTurn(RaceState state, HorseInRace horseToPlay)
    {
        state.IncrementTurnIfApplicable();
        state.IncrementNextInTurnIfApplicable();
        horseToPlay.CleanupTurn();
    }
}