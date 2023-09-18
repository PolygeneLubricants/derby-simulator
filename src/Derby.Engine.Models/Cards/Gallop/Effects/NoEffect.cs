namespace Derby.Engine.Models.Cards.Gallop.Effects;

public class NoEffect : IGallopCardEffect
{
    public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        return new GallopCardResolution();
    }
}