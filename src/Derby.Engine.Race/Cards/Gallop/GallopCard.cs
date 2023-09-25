using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop;

/// <summary>
///     Gallop-card implementation of the base card.
/// </summary>
public class GallopCard : BaseCard<GallopCardResolution>
{
    /// <inheritdoc />
    public override GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        return CardEffect.Resolve(horseToPlay, state);
    }
}