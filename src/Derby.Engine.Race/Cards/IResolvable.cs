namespace Derby.Engine.Race.Cards;

public interface IResolvable<TResolution>
{
    TResolution Resolve(HorseInRace horseToPlay, RaceState state);
}