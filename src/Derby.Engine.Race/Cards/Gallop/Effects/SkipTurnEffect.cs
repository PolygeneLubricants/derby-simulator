namespace Derby.Engine.Race.Cards.Gallop.Effects;

public class SkipTurnEffect : IGallopCardEffect
{
    public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        return new GallopCardResolution { EndTurn = true };
    }
}