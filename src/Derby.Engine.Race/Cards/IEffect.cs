using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards;

/// <summary>
///     Represents an effect (card effect) entity that can be resolved.
/// </summary>
public interface IEffect<out TResolution> where TResolution : IEffectResolution
{
    /// <summary>
    ///     Resolves the current effect by <paramref name="horseToPlay" /> and applied on <paramref name="state" />.
    /// </summary>
    /// <param name="horseToPlay">The owner or invoker of this effect.</param>
    /// <param name="state">The state to apply the effect on.</param>
    TResolution Resolve(HorseInRace horseToPlay, RaceState state);
}