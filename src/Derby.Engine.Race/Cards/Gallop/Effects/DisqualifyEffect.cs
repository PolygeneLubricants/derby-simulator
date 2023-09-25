using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop.Effects;

/// <summary>
///     Effect that disqualifies the horse, rendering it unable to move or win the race.
///     Similar to <see cref="Derby.Engine.Race.Cards.Chance.Effects.EliminateHorseEffect" />.
/// </summary>
public class DisqualifyEffect : IGallopCardEffect
{
    public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        horseToPlay.Eliminate();
        return new GallopCardResolution { HorseDisqualified = true };
    }
}