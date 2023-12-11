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
    ///     The number of horses behind at the time of applying the modifier.
    /// </summary>
    private int _initialHorsesBehind;

    /// <summary>
    ///     The number of horses behind which will trigger the end of the await.
    /// </summary>
    private int _targetHorsesBehind;

    /// <summary>
    ///     Number of eliminated horses behind at the time of applying the modifier.
    ///     This is important, as horses could be eliminated whilst awaiting. In this case, the eliminated
    ///     horse(s) will not count towards the number of horses behind.
    /// </summary>
    private int _eliminatedHorsesBehind;

    public AwaitModifier(AwaitType awaitType)
    {
        _awaitType = awaitType;
    }

    public void Initialize(HorseInRace horseWithModifier, RaceState state)
    {
        var horsesBehind = state.GetHorsesBehind(horseWithModifier);
        _eliminatedHorsesBehind = horsesBehind.Count(horse => horse.Eliminated);
        _initialHorsesBehind = horsesBehind.Count();
        _targetHorsesBehind = _awaitType switch
        {
            AwaitType.Nearest => _initialHorsesBehind - 1,
            AwaitType.Last    => 0,
            AwaitType.All     => 0,
            _                 => throw new ArgumentOutOfRangeException()
        };
    }

    public ModifierResolution Apply(HorseInRace horseWithModifier, RaceState state)
    {
        if (HorseToAwaitHasCaughtUp(state, horseWithModifier))
        {
            return new ModifierResolution { IsApplicable = false };
        }

        return new ModifierResolution { IsApplicable = true, EndTurn = true };
    }

    private bool HorseToAwaitHasCaughtUp(RaceState state, HorseInRace horseWithModifier)
    {
        if(_initialHorsesBehind - _eliminatedHorsesBehind <= 0)
        {
            return true;
        }

        var horsesBehind = state.GetHorsesBehind(horseWithModifier);
        return (horsesBehind.Count() - horsesBehind.Where(h => h.Eliminated).Count()) <= (_targetHorsesBehind - horsesBehind.Where(h => h.Eliminated).Count());
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