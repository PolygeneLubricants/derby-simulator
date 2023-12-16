using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop.Effects;

/// <summary>
///     Card effect, that only triggers its inner effect, if its condition is met.
/// </summary>
public class ConditionalEffect : IGallopCardEffect
{
    private readonly Condition         _condition;
    private readonly IGallopCardEffect _effectIfConditionIsMet;

    /// <summary>
    ///     Constructor for conditional effect.
    /// </summary>
    /// <param name="condition">The condition that must be met for <paramref name="effectIfConditionIsMet" /> to take effect.</param>
    /// <param name="effectIfConditionIsMet">The condition that triggers if <paramref name="condition" /> is met.</param>
    public ConditionalEffect(Condition condition, IGallopCardEffect effectIfConditionIsMet)
    {
        _condition = condition;
        _effectIfConditionIsMet = effectIfConditionIsMet;
    }

    public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        if (ConditionIsMet(horseToPlay, state))
        {
            return _effectIfConditionIsMet.Resolve(horseToPlay, state);
        }

        return new GallopCardResolution();
    }

    private bool ConditionIsMet(HorseInRace horseToPlay, RaceState state)
    {
        switch (_condition)
        {
            case Condition.IsNotLeader:
                var leader = state.GetLeaderHorse();
                return horseToPlay != leader;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public enum Condition
{
    /// <summary>
    ///     Condition that applies if the horse is not in the lead.
    /// </summary>
    IsNotLeader
}