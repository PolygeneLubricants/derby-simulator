using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Chance;

/// <summary>
///     Chance-card specific card.
/// </summary>
public class ChanceCard : BaseCard<ChanceCardResolution>
{
    public override ChanceCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        return CardEffect.Resolve(horseToPlay, state);
    }
}