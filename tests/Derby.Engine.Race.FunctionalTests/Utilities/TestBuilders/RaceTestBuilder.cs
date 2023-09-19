using Derby.Engine.Race.Board;
using Derby.Engine.Race.Board.Lanes.Fields;
using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;

public class RaceTestBuilder
{
    private readonly GameBoard _board;
    private readonly ChanceDeck _chanceDeck;
    private readonly GallopDeck _gallopDeck;
    private readonly IList<OwnedHorse> _horsesInRace;
    private readonly IList<StableCode> _availableStables;

    public RaceTestBuilder()
    {
        _availableStables = Enum.GetValues<StableCode>().ToList();
        _horsesInRace = new List<OwnedHorse>();
        _board = new GameBoard { Lanes = new LaneCollection() };
        _chanceDeck = new ChanceDeck();
        _gallopDeck = new GallopDeck();
    }

    public Race Build()
    {
        var state = new RaceState(_board, _horsesInRace)
        {
            ChanceDeck = _chanceDeck,
            GallopDeck = _gallopDeck
        };

        return new Race
        {
            State = state
        };
    }

    public RaceTestBuilder WithHorseInRace(IEnumerable<int> moves)
    {
        var stableCode = _availableStables[0];
        _availableStables.RemoveAt(0);

        var ownedHorse = new OwnedHorse
        {
            Horse = new Horse
            {
                Color = Color.Black,
                Moves = moves.ToList(),
                Years = 2,
                Name = Guid.NewGuid().ToString()
            },
            Owner = new Player
            {
                Stable = new Stable
                {
                    Code = stableCode
                }
            }
        };

        _horsesInRace.Add(ownedHorse);

        return this;
    }

    public RaceTestBuilder WithLane(int length)
    {
        var fields =
            Enumerable.Range(0, 1).Select(_ => new StartField())
                .Concat<IField>(Enumerable.Range(0, length - 2).Select(_ => new NeutralField()))
                .Concat(Enumerable.Range(0, 1).Select(_ => new GoalField())).ToList();

        _board.Lanes = new LaneCollection
        {
            Lane2Years = new CustomLane(fields)
        };

        return this;
    }
}