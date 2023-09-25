using Derby.Engine.Race.Cards.Gallop.Effects.Modifiers.Exceptions;
using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

/// <summary>
///     Await modifier, which stalls a horse until another horse has caught up.
///     The horse to await is indicated in <see cref="AwaitType" />.
///     If the horse to await is eliminated, the await modifier is no longer applicable.
/// </summary>
public class AwaitModifier : IModifier
{
    /// <summary>
    ///     The type of await.
    /// </summary>
    private readonly AwaitType _awaitType;

    /// <summary>
    ///     The horse to await.
    /// </summary>
    private HorseInRace? _horseToAwait;

    public AwaitModifier(AwaitType awaitType)
    {
        _awaitType = awaitType;
    }

    public void Initialize(HorseInRace horseWithModifier, RaceState state)
    {
        _horseToAwait = _awaitType switch
        {
            AwaitType.Last    => state.GetLastHorse(),
            AwaitType.Nearest => state.GetHorseBehind(horseWithModifier) ?? horseWithModifier,
            AwaitType.All     => null,
            _                 => throw new ArgumentOutOfRangeException()
        };
    }

    public ModifierResolution Apply(HorseInRace horseWithModifier, RaceState state)
    {
        // Handle edge-case, when horse is awaiting a horse which has then been eliminated.
        // In this case, the horse is soft-locked if not handled like this.
        // Note, the rules do not specify this situation, as an alternate interpretation could be,
        // to pick the _next_ horse that fits the card criteria.
        if (_horseToAwait is { Eliminated: true })
        {
            return new ModifierResolution { IsApplicable = false };
        }

        if (_awaitType == AwaitType.All)
        {
            if (state.GetLastHorse().GetLaneTiebreaker() >= horseWithModifier.GetLaneTiebreaker())
            {
                return new ModifierResolution { IsApplicable = false };
            }

            return new ModifierResolution { IsApplicable = true, EndTurn = true };
        }

        if (_horseToAwait == null)
        {
            throw new ModifierNotInitializedException();
        }

        if (HorseToAwaitHasCaughtUp(_horseToAwait, horseWithModifier))
        {
            return new ModifierResolution { IsApplicable = false };
        }

        return new ModifierResolution { IsApplicable = true, EndTurn = true };
    }

    private bool HorseToAwaitHasCaughtUp(HorseInRace horseToAwait, HorseInRace horseWithModifier)
    {
        return horseToAwait.GetLaneTiebreaker() >= horseWithModifier.GetLaneTiebreaker();
    }
}

/// <summary>
///     Indicates the mechanism that applies when awaiting a horse.
/// </summary>
public enum AwaitType
{
    /// <summary>
    ///     Horse with modifier will await the last horse in the race at the time of drawing/applying the modifier.
    /// </summary>
    Last,

    /// <summary>
    ///     Horse with modifier will await the horse immediately behind this in the race at the time of drawing/applying the
    ///     modifier.
    /// </summary>
    Nearest,

    /// <summary>
    ///     Horse with modifier will await all horses behind this.
    /// </summary>
    All
}