namespace Derby.Engine.Models.Cards;

public interface IEffect<TResolution>
{
    TResolution Resolve(HorseInRace horseToPlay, RaceState state);
}