using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Chance.Effects;

/// <summary>
///     Card effect that does nothing.
///     Similar to <see cref="Gallop.Effects.NoEffect" />."/>
/// </summary>
public class NoEffect : IChanceCardEffect
{
    public ChanceCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        return new ChanceCardResolution();
    }
}