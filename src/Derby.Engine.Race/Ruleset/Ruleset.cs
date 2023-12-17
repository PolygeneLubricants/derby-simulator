using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Gallop;

namespace Derby.Engine.Race.Ruleset;

/// <summary>
///     Defines the variable parts of the engine, specified by a ruleset.
/// </summary>
public class Ruleset
{
    /// <summary>
    ///     The Gallop deck used in this ruleset.
    /// </summary>
    public GallopDeck GallopDeck { get; init; }

    /// <summary>
    ///     The chance deck used in this ruleset.
    /// </summary>
    public ChanceDeck ChanceDeck { get; init; }
}