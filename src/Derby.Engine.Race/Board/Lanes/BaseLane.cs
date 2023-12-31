﻿using Derby.Engine.Race.Board.Lanes.Fields;

namespace Derby.Engine.Race.Board.Lanes;

/// <inheritdoc />
public abstract class BaseLane : ILane
{
    protected BaseLane(IList<IField> fields)
    {
        Fields = fields;
    }

    /// <inheritdoc />
    public IList<IField> Fields { get; }

    /// <inheritdoc />
    public int GetTiebreaker(int horseLocation)
    {
        return Fields[horseLocation].TieBreaker;
    }

    /// <inheritdoc />
    public int GetClosestLocation(int tieBreakToSeek, SeekerStrategy strategy)
    {
        switch (strategy)
        {
            case SeekerStrategy.Before:
                for (var i = 0; i < Fields.Count; i++)
                {
                    if (Fields[i].TieBreaker < tieBreakToSeek)
                    {
                        continue;
                    }

                    if (i == 0)
                    {
                        return 0;
                    }

                    return i - 1;
                }

                return Fields.IndexOf(Fields.Last());
            case SeekerStrategy.Closest:
                for (var i = 0; i < Fields.Count; i++)
                {
                    if (Fields[i].TieBreaker < tieBreakToSeek)
                    {
                        continue;
                    }

                    if (i == 0)
                    {
                        return 0;
                    }

                    var diffFromGreater = Math.Abs(Fields[i].TieBreaker - tieBreakToSeek);
                    var diffFromLower   = Math.Abs(Fields[i - 1].TieBreaker - tieBreakToSeek);
                    if (diffFromGreater < diffFromLower)
                    {
                        return i;
                    }

                    return i - 1;
                }

                return Fields.IndexOf(Fields.Last());
            default:
                throw new ArgumentOutOfRangeException(nameof(strategy), strategy, null);
        }
    }
}