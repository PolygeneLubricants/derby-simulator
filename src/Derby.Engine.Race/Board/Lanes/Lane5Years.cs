using Derby.Engine.Race.Board.Lanes.Fields;

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
            new StartField(0),
            new NeutralField(100),
            new GallopField(200),
            new NeutralField(300),
            new ChanceField(400),
            new NeutralField(500),
            new GallopField(600),
            // Curve start
            new NeutralField(645),
            new NeutralField(691),
            new NeutralField(736),
            new NeutralField(782),
            new NeutralField(827),
            new ChanceField(873),
            new NeutralField(918),
            new NeutralField(964),
            new NeutralField(1009),
            new NeutralField(1055),
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
            new NeutralField(1945),
            new NeutralField(1991),
            new NeutralField(2036),
            new NeutralField(2082),
            new NeutralField(2127),
            new GallopField(2173),
            new NeutralField(2218),
            new NeutralField(2264),
            new NeutralField(2309),
            new NeutralField(2355),
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