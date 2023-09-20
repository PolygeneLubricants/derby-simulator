namespace Derby.Engine.Race.Cards.Gallop.Effects;

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
        else
        {
            return _effectWhenNotHomestretch.Resolve(horseToPlay, state);
        }
    }
}