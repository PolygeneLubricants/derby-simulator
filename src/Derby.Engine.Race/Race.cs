using Derby.Engine.Race.Board;
using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Turns;
using Derby.Engine.Race.Turns.Resolutions;

namespace Derby.Engine.Race;

public class Race
{
    public event Action<HorseInRace> TurnStarted = (horseInRace => {});
    
    public static Race GetDefault(IEnumerable<OwnedHorse> horsesToRace)
    {
        var race = new Race()
        {
            State = new RaceState(GameBoard.DefaultBoard(), horsesToRace)
            {
                GallopDeck = GallopDeck.DefaultDeck(),
                ChanceDeck = ChanceDeck.DefaultDeck(),
            }
        };

        return race;
    }

    public Race()
    {
        TurnResolver = new TurnResolver();
    }

    public required RaceState State { get; init; }

    public readonly TurnResolver TurnResolver;
    
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

    public ITurnResolution? FinalResolution { get; private set; }

    public bool GameEnded { get; private set; }

    private void CleanupTurn(RaceState state, HorseInRace? horseToPlay)
    {
        state.IncrementTurnIfApplicable();
        state.IncrementNextInTurnIfApplicable();
        horseToPlay?.CleanupTurn();
    }
}