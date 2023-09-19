using Derby.Engine.Race.Board.Lanes.Fields;

namespace Derby.Engine.Race.Board.Lanes;

public class Lane4Years : BaseLane
{
    public Lane4Years() : base(PopulateLane())
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
            new ChanceField(),
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
            new GallopField(),
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