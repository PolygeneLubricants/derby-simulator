namespace Derby.Engine.Race.Cards.Chance;

/// <summary>
///     Chance-card specific resolution.
/// </summary>
public class ChanceCardResolution : IEffectResolution
{
    /// <summary>
    ///     Will be true if the chance card resolution results in the horse being eliminated.
    /// </summary>
    public bool IsHorseEliminated { get; set; }
}