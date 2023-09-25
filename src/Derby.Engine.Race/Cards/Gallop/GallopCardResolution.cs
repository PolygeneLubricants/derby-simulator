using Derby.Engine.Race.Board.Lanes.Fields;

namespace Derby.Engine.Race.Cards.Gallop;

/// <summary>
///     Gallop-card specific effect resolution.
/// </summary>
public class GallopCardResolution : IEffectResolution
{
    /// <summary>
    ///     Value will be non-null if the gallop card results in the horse landing on a new field.
    /// </summary>
    public IField? NewField { get; set; }

    /// <summary>
    ///     Value will be true if the gallop card results in the horse's turn ending.
    /// </summary>
    public bool EndTurn { get; set; }

    /// <summary>
    ///     Value will be true if the gallop card results in the horse being disqualified/eliminated.
    /// </summary>
    public bool HorseDisqualified { get; set; }
}