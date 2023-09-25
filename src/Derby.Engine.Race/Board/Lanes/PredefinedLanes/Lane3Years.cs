using Derby.Engine.Race.Board.Lanes.Fields;

namespace Derby.Engine.Race.Board.Lanes.PredefinedLanes;

public class Lane3Years : BaseLane
{
    public Lane3Years() : base(PopulateLane())
    {
    }

    private static IList<IField> PopulateLane()
    {
        return new List<IField>
        {
            new StartField(0),
            new NeutralField(100),
            new GallopField(200),
            new NeutralField(300),
            new ChanceField(400),
            new NeutralField(500),
            new GallopField(600),
            // Curve start
            new NeutralField(671),
            new NeutralField(742),
            new NeutralField(814),
            new ChanceField(885),
            new NeutralField(957),
            new NeutralField(1028),
            new NeutralField(1100),
            // Curve end
            new GallopField(1200),
            new NeutralField(1300),
            new ChanceField(1400),
            new NeutralField(1500),
            new NeutralField(1600),
            new GallopField(1700),
            new NeutralField(1800),
            new ChanceField(1900),
            // Curve start
            new NeutralField(1971),
            new NeutralField(2043),
            new NeutralField(2114),
            new GallopField(2186),
            new NeutralField(2257),
            new NeutralField(2329),
            new NeutralField(2400),
            // Curve end
            new ChanceField(2500),
            new NeutralField(2600),
            new GallopField(2700),
            new NeutralField(2800),
            new ChanceField(2900),
            new NeutralField(3000),
            new NeutralField(3100),
            new GoalField(3200)
        };
    }
}