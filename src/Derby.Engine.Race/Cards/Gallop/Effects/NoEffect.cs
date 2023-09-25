using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop.Effects;

/// <summary>
///     Card effect that does nothing.
///     Similar to <see cref="Chance.Effects.NoEffect" />.
/// </summary>
public class NoEffect : IGallopCardEffect
{
    public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        return new GallopCardResolution();
    }
}