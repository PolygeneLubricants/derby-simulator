using Derby.Engine.Race.Ruleset.Variations.Derby2023;
using Derby.Engine.Race.Ruleset.Variations.Drechsler;

namespace Derby.Engine.Race.Ruleset;

/// <summary>
///     Ruleset provider, that returns the ruleset that applies to a given ruleset type.
/// </summary>
public class RulesetProvider
{
    public Ruleset Get(RulesetType type)
    {
        return type switch
        {
            RulesetType.Drechsler            => CreateDrechslerRuleset(),
            RulesetType.Derby2023Traditional => CreateDerby2023TraditionalRuleset(),
            _                                => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    private Ruleset CreateDrechslerRuleset()
    {
        return new Ruleset
        {
            ChanceDeck = DrechslerChanceDeck.Deck,
            GallopDeck = DrechslerGallopDeck.Deck
        };
    }

    private Ruleset CreateDerby2023TraditionalRuleset()
    {
        return new Ruleset
        {
            ChanceDeck = Derby2023ChanceDeck.Deck,
            GallopDeck = Derby2023GallopDeck.Deck
        };
    }
}