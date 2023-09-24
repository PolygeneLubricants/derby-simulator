using Derby.Engine.Race.Board.Lanes;
using Derby.Engine.Race.Board.Lanes.Fields;
using Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

namespace Derby.Engine.Race;

public class HorseInRace
{
    public HorseInRace()
    {
        Location = 0;
        Eliminated = false;
        HasMovedThisTurn = false;
        Modifiers = new List<IModifier>();
    }

    public required OwnedHorse OwnedHorse { get; init; }

    public required ILane Lane { get; set; }

    public bool Eliminated { get; private set; }

    public int Location { get; private set; }

    public int FieldsFromGoal => Lane.Fields.Count - Location - 1;

    public int GetLaneTiebreaker() => Lane.GetTiebreaker(Location);

    public IList<IModifier> Modifiers { get; set; }

    public bool HasMovedThisTurn { get; set; }

    public int GallopCardsDrawn { get; set; }

    public int ChanceCardsDrawn { get; set; }

    public void Eliminate()
    {
        Eliminated = true;
    }
    
    public bool TurnIsHomeStretch(int turnNumber)
    {
        if (HasMovedThisTurn)
        {
            return false;
        }

        var moves = OwnedHorse.Horse.GetMoves(turnNumber);
        return moves >= FieldsFromGoal;
    }

    public IField Move(int moves, MoveType moveType)
    {
        if (moveType == MoveType.Natural)
        {
            HasMovedThisTurn = true;
        }

        Location += moves;
        
        if (Location >= Lane.Fields.Count)
        {
            Location = Lane.Fields.Count - 1;
        }

        var fieldHorseLandedOn = Lane.Fields[Location];
        return fieldHorseLandedOn;
    }

    public void CleanupTurn()
    {
        HasMovedThisTurn = false;
    }
}

public enum MoveType
{
    CardEffect, // Movement caused by a card effect.
    Natural // Movement caused by the horse's natural movement.
}