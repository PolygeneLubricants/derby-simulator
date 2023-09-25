using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop.Effects;

/// <summary>
///     Effect that has different behavior depending on whether the horse is in the home stretch or not.
/// </summary>
public class HomeStretchCompositeEffect : IGallopCardEffect
{
    private readonly IGallopCardEffect _effectWhenHomestretch;
    private readonly IGallopCardEffect _effectWhenNotHomestretch;

    public HomeStretchCompositeEffect(
        IGallopCardEffect effectWhenHomestretch,
        IGallopCardEffect effectWhenNotHomestretch)
    {
        _effectWhenHomestretch = effectWhenHomestretch;
        _effectWhenNotHomestretch = effectWhenNotHomestretch;
    }

    public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        if (horseToPlay.TurnIsHomeStretch(state.CurrentTurn))
        {
            return _effectWhenHomestretch.Resolve(horseToPlay, state);
        }

        return _effectWhenNotHomestretch.Resolve(horseToPlay, state);
    }
}