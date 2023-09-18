namespace Derby.Engine.Models.Cards.Gallop.Effects;

public class DisqualifyEffect : IGallopCardEffect
{
    public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        horseToPlay.Eliminate();
        return new GallopCardResolution { HorseDisqualified = true };
    }
}