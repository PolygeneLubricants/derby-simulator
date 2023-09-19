namespace Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

public class AwaitModifier : IModifier
{
    private readonly AwaitType _awaitType;

    private HorseInRace? _horseToAwait;

    public AwaitModifier(AwaitType awaitType)
    {
        _awaitType = awaitType;
    }

    public void Initialize(HorseInRace horseWithModifier, RaceState state)
    {
        _horseToAwait = _awaitType switch
        {
            AwaitType.Last => state.GetLastHorse(),
            AwaitType.Nearest => state.GetHorseBehind(horseWithModifier) ?? horseWithModifier,
            AwaitType.All => null,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public ModifierResolution Apply(HorseInRace horseWithModifier, RaceState state)
    {
        if (_awaitType == AwaitType.All)
        {
            if (state.GetLastHorse().GetLaneTiebreaker() >= horseWithModifier.GetLaneTiebreaker())
            {
                return new ModifierResolution { IsApplicable = false };
            }
            else
            {
                return new ModifierResolution { IsApplicable = true, EndTurn = true };
            }
        }

        if (_horseToAwait == null)
        {
            throw new ModifierNotInitializedException();
        }

        if (HorseToAwaitHasCaughtUp(_horseToAwait, horseWithModifier))
        {
            return new ModifierResolution { IsApplicable = false };
        }
        else
        {
            return new ModifierResolution { IsApplicable = true, EndTurn = true };
        }
    }

    private bool HorseToAwaitHasCaughtUp(HorseInRace horseToAwait, HorseInRace horseWithModifier)
    {
        return horseToAwait.GetLaneTiebreaker() >= horseWithModifier.GetLaneTiebreaker();
    }
}

public enum AwaitType
{
    Last,
    Nearest,
    All
}