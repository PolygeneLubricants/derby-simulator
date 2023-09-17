namespace Derby.Engine.Models.Cards;

public interface IResolvable<TResolution>
{
    TResolution Resolve(HorseInRace horseToPlay, RaceState state);
}