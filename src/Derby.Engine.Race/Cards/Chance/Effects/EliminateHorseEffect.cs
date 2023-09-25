using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Chance.Effects;

/// <summary>
/// Card effect that eliminates a horse from the race. Similar to <see cref="Derby.Engine.Race.Cards.Gallop.Effects.DisqualifyEffect"/>.
/// </summary>
public class EliminateHorseEffect : IChanceCardEffect
{
    public ChanceCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        horseToPlay.Eliminate();
        return new ChanceCardResolution { IsHorseEliminated = true };
    }
}