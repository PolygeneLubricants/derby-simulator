namespace Derby.Engine.Models.Cards.Chance.Effects;

public class NoEffect : IChanceCardEffect
{
    public ChanceCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        return new ChanceCardResolution();
    }
}