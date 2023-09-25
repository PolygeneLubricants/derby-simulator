using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards;

public interface IEffect<TResolution>
{
    TResolution Resolve(HorseInRace horseToPlay, RaceState state);
}