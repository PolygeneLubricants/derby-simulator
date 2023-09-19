﻿using Derby.Engine.Race.Board.Lanes.Fields;

namespace Derby.Engine.Race.Board.Lanes;

public class Lane5Years : BaseLane
{
    public Lane5Years() : base(PopulateLane())
    {
    }

    private static IList<IField> PopulateLane()
    {
        return new List<IField>
        {
            new StartField(),
            new NeutralField(),
            new GallopField(),
            new NeutralField(),
            new ChanceField(),
            new NeutralField(),
            new GallopField(),
            new NeutralField(),
            new NeutralField(),
            new NeutralField(),
            new NeutralField(),
            new NeutralField(),
            new ChanceField(),
            new NeutralField(),
            new NeutralField(),
            new NeutralField(),
            new NeutralField(),
            new NeutralField(),
            new GallopField(),
            new NeutralField(),
            new ChanceField(),
            new NeutralField(),
            new NeutralField(),
            new GallopField(),
            new NeutralField(),
            new ChanceField(),
            new NeutralField(),
            new NeutralField(),
            new NeutralField(),
            new NeutralField(),
            new NeutralField(),
            new GallopField(),
            new NeutralField(),
            new NeutralField(),
            new NeutralField(),
            new NeutralField(),
            new NeutralField(),
            new ChanceField(),
            new NeutralField(),
            new GallopField(),
            new NeutralField(),
            new ChanceField(),
            new NeutralField(),
            new NeutralField(),
            new GoalField()
        };
    }
}