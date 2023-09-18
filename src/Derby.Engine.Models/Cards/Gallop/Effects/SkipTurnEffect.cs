namespace Derby.Engine.Models.Cards.Gallop.Effects;

public class SkipTurnEffect : IGallopCardEffect
{
    public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        return new GallopCardResolution { EndTurn = true };
    }
}