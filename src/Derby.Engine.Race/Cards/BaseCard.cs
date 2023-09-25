using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards;

/// <summary>
///     Base class for all cards.
/// </summary>
/// <typeparam name="TResolution">Generic resolution type, which varies depending on the implementing card type.</typeparam>
public abstract class BaseCard<TResolution> where TResolution : IEffectResolution
{
    /// <summary>
    ///     Human readable title/headline of the card.
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    ///     Human readable text-description of the card.
    /// </summary>
    public required string Description { get; init; }

    /// <summary>
    ///     The programmatic effect of the card, which is applied when the card is resolved.
    /// </summary>
    public required IEffect<TResolution> CardEffect { get; init; }

    /// <summary>
    ///     Resolves the card, and applies the <see cref="CardEffect" /> to the <paramref name="state" /> for
    ///     <paramref name="horseToPlay" />.
    /// </summary>
    public abstract IEffectResolution Resolve(HorseInRace horseToPlay, RaceState state);
}