using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Chance.Effects;

public class EliminateHorseEffect : IChanceCardEffect
{
    public ChanceCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        horseToPlay.Eliminate();
        return new ChanceCardResolution { IsHorseEliminated = true };
    }
}