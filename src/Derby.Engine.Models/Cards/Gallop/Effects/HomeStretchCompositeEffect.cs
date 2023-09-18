namespace Derby.Engine.Models.Cards.Gallop.Effects;

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
        var moves = horseToPlay.OwnedHorse.Horse.GetMoves(state.CurrentTurn);
        if (horseToPlay.NextTurnIsHomeStretch(moves))
        {
            return _effectWhenHomestretch.Resolve(horseToPlay, state);
        }
        else
        {
            return _effectWhenNotHomestretch.Resolve(horseToPlay, state);
        }
    }
}