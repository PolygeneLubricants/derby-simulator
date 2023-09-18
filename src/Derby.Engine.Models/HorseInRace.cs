﻿using Derby.Engine.Models.Board.Lanes;
using Derby.Engine.Models.Board.Lanes.Fields;
using Derby.Engine.Models.Cards.Gallop.Effects.Modifiers;

namespace Derby.Engine.Models;

public class HorseInRace
{
    public HorseInRace()
    {
        Location = 0;
        Eliminated = false;
        Modifiers = new List<IModifier>();
    }

    public required OwnedHorse OwnedHorse { get; init; }

    public required ILane Lane { get; set; }

    public bool Eliminated { get; private set; }

    public int Location { get; private set; }

    public int FieldsFromGoal => Lane.Fields.Count - Location - 1;
    public IList<IModifier> Modifiers { get; set; }

    public void Eliminate()
    {
        Eliminated = true;
    }
    
    public bool NextTurnIsHomeStretch(int turnNumber)
    {
        var moves = OwnedHorse.Horse.GetMoves(turnNumber);
        return moves >= FieldsFromGoal;
    }

    public IField Move(int moves)
    {
        Location += moves;
        
        if (Location >= Lane.Fields.Count)
        {
            Location = Lane.Fields.Count - 1;
        }

        var fieldHorseLandedOn = Lane.Fields[Location];
        return fieldHorseLandedOn;
    }
}