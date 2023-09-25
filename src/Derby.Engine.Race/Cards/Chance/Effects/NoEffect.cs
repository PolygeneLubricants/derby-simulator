using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Chance.Effects;

public class NoEffect : IChanceCardEffect
{
    public ChanceCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        return new ChanceCardResolution();
    }
}