using Derby.Engine.Models.Board.Lanes.Fields;

namespace Derby.Engine.Models.Board.Lanes;

public class Lane3Years : BaseLane
{
    protected override IList<IField> PopulateLane()
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
            new ChanceField(),
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
            new GallopField(),
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