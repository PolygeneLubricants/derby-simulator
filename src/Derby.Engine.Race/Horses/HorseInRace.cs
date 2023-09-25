using Derby.Engine.Race.Board.Lanes;
using Derby.Engine.Race.Board.Lanes.Fields;
using Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

namespace Derby.Engine.Race.Horses;

/// <summary>
///     A horse associated with a race.
/// </summary>
public class HorseInRace
{
    public HorseInRace()
    {
        Location = 0;
        Eliminated = false;
        HasMovedThisTurn = false;
        Modifiers = new List<IModifier>();
    }

    /// <summary>
    ///     The owner (and horse) association of the horse in this race.
    /// </summary>
    public required OwnedHorse OwnedHorse { get; init; }

    /// <summary>
    ///     The lane which this horse has been registered in.
    /// </summary>
    public required ILane Lane { get; set; }

    /// <summary>
    ///     Indicates whether this horse has been eliminated throughout the race.
    /// </summary>
    public bool Eliminated { get; private set; }

    /// <summary>
    ///     The current location (array index) of the horse on the lane.
    /// </summary>
    public int Location { get; private set; }

    /// <summary>
    ///     The number of fields between the horse and the goal.
    /// </summary>
    public int FieldsFromGoal => Lane.Fields.Count - Location - 1;

    /// <summary>
    ///     The current modifiers on the horse.
    ///     The list of modifiers will need to be applied in order to determine if
    ///     they still apply.
    /// </summary>
    public IList<IModifier> Modifiers { get; set; }

    /// <summary>
    ///     Indicates whether the horse has moved this turn or not.
    /// </summary>
    public bool HasMovedThisTurn { get; set; }

    /// <summary>
    ///     The number of Gallop cards this horse has drawn within this race.
    /// </summary>
    public int GallopCardsDrawn { get; set; }

    /// <summary>
    ///     The number of Chance cards this horse has drawn within this race.
    /// </summary>
    public int ChanceCardsDrawn { get; set; }

    /// <summary>
    ///     Gets the lane tie breaker which the horse is currently standing at.
    ///     The tier breaker is a static association to the current lane location.
    /// </summary>
    /// <returns></returns>
    public int GetLaneTiebreaker()
    {
        return Lane.GetTiebreaker(Location);
    }

    /// <summary>
    ///     Eliminate this horse from the race.
    /// </summary>
    public void Eliminate()
    {
        Eliminated = true;
    }

    /// <summary>
    ///     Indicates whether the horse is in the home stretch when it will move naturally.
    /// </summary>
    public bool TurnIsHomeStretch(int turnNumber)
    {
        if (HasMovedThisTurn)
        {
            return false;
        }

        var moves = OwnedHorse.Horse.GetMoves(turnNumber);
        return moves >= FieldsFromGoal;
    }

    /// <summary>
    ///     Move the horse a number of fields.
    /// </summary>
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

    /// <summary>
    ///     Cleanup the horse state at the end of its turn.
    /// </summary>
    public void CleanupTurn()
    {
        HasMovedThisTurn = false;
    }
}

/// <summary>
///     When the horse moves, the type will change depending on the cause of the movement.
/// </summary>
public enum MoveType
{
    CardEffect, // Movement caused by a card effect.
    Natural     // Movement caused by the horse's natural movement.
}