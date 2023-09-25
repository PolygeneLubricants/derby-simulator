using Derby.Engine.Race.Board.Lanes.Fields;

namespace Derby.Engine.Race.Board.Lanes.PredefinedLanes;

public class Lane4Years : BaseLane
{
    public Lane4Years() : base(PopulateLane())
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
            new NeutralField(656),
            new NeutralField(711),
            new NeutralField(767),
            new NeutralField(822),
            new ChanceField(878),
            new NeutralField(933),
            new NeutralField(989),
            new NeutralField(1044),
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
            new NeutralField(1956),
            new NeutralField(2011),
            new NeutralField(2067),
            new NeutralField(2122),
            new GallopField(2178),
            new NeutralField(2233),
            new NeutralField(2289),
            new NeutralField(2344),
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