namespace Derby.Engine.Race.Cards;

public abstract class BaseCard<TResolution, TEffect> : IResolvable<TResolution> where TEffect : IEffect<TResolution>
{
    public required string Title { get; init; }

    public required string Description { get; init; }

    public required TEffect CardEffect { get; init; }

    public abstract TResolution Resolve(HorseInRace horseToPlay, RaceState state);
}