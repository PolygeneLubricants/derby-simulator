﻿using Derby.Engine.Race.Board;
using Derby.Engine.Race.Board.Lanes.Fields;
using Derby.Engine.Race.Board.Lanes.PredefinedLanes;
using Derby.Engine.Race.Cards;
using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.FunctionalTests.Utilities.TestModels;
using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.FunctionalTests.Utilities.TestBuilders;

public class RaceTestBuilder
{
    private readonly GameBoard _board;
    private ChanceDeck _chanceDeck;
    private GallopDeck _gallopDeck;
    private readonly IList<OwnedHorse> _horsesInRace;
    private readonly IList<StableCode> _availableStables;

    public RaceTestBuilder()
    {
        _availableStables = Enum.GetValues<StableCode>().ToList();
        _horsesInRace = new List<OwnedHorse>();
        _board = new GameBoard { Lanes = new LaneCollection
        {
            Lane2Years = new CustomLane(new List<IField>()),
            Lane3Years = new CustomLane(new List<IField>()),
            Lane4Years = new CustomLane(new List<IField>()),
            Lane5Years = new CustomLane(new List<IField>())
        }
        };
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

    public RaceTestBuilder WithHorseInRace(IEnumerable<int> moves, out OwnedHorse horseAdded)
    {
        return WithHorseInRace(moves, 2, out horseAdded);
    }

    public RaceTestBuilder WithHorseInRace(IEnumerable<int> moves, int years, out OwnedHorse horseAdded)
    {
        var stableCode = _availableStables[0];
        _availableStables.RemoveAt(0);

        var ownedHorse = new OwnedHorse
        {
            Horse = new Horse
            {
                Color = Color.Black,
                Moves = moves.ToList(),
                Years = years,
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
        horseAdded = ownedHorse;

        return this;
    }

    public RaceTestBuilder WithHorseInRace(IEnumerable<OwnedHorse> horsesInRace)
    {
        foreach (var horse in horsesInRace)
        {
            _horsesInRace.Add(horse);
        }

        return this;
    }

    public RaceTestBuilder WithLane(int length)
    {
        return WithLane(length, 2);
    }

    public RaceTestBuilder WithLane(int length, int years)
    {
        var startField = Enumerable.Range(0, 1).Select(index => new StartField(index * 100)).ToList();
        var neutralFields = Enumerable.Range(0, length - 2).Select(index => new NeutralField((1+index) * 100)).ToList();
        var goalField = Enumerable.Range(0, 1).Select(index => new GoalField((startField.Count + neutralFields.Count + index) * 100)).ToList();
        var fields = startField.Concat<IField>(neutralFields).Concat<IField>(goalField).ToList();

        return WithLane(fields, years);
    }

    public RaceTestBuilder WithPredefinedLanes()
    {
        _board.Lanes.Lane2Years = new Lane2Years();
        _board.Lanes.Lane3Years = new Lane3Years();
        _board.Lanes.Lane4Years = new Lane4Years();
        _board.Lanes.Lane5Years = new Lane5Years();
        return this;
    }

    public RaceTestBuilder WithLane(IList<IField> fields, int years)
    {
        switch (years)
        {
            case 2:
                _board.Lanes.Lane2Years = new CustomLane(fields);
                break;
            case 3:
                _board.Lanes.Lane3Years = new CustomLane(fields);
                break;
            case 4:
                _board.Lanes.Lane4Years = new CustomLane(fields);
                break;
            case 5:
                _board.Lanes.Lane5Years = new CustomLane(fields);
                break;
            default:
                throw new ArgumentOutOfRangeException($"{years}");
        }

        return this;
    }

    public RaceTestBuilder WithNoEffectGallopCard()
    {
        var card = new GallopCard
        {
            Title = "",
            Description = "",
            CardEffect = new NoEffect()
        };

        return WithGallopCard(card);
    }

    public RaceTestBuilder WithGallopCard(GallopCard card)
    {
        _gallopDeck.Deck.Add(card);

        return this;
    }

    public RaceTestBuilder WithNoEffectChanceCard()
    {
        var card = new ChanceCard
        {
            Title = "",
            Description = "",
            CardEffect = new Cards.Chance.Effects.NoEffect()
        };
        return WithChanceCard(card);
    }

    public RaceTestBuilder WithChanceCard(ChanceCard chanceCard)
    {
        _chanceDeck.Deck.Add(chanceCard);
        return this;
    }

    public RaceTestBuilder WithGallopDeck(GallopDeck gallopDeckInRace)
    {
        _gallopDeck = gallopDeckInRace;
        return this;
    }

    public RaceTestBuilder WithChanceDeck(ChanceDeck chanceDeckInRace)
    {
        _chanceDeck = chanceDeckInRace;
        return this;
    }
}