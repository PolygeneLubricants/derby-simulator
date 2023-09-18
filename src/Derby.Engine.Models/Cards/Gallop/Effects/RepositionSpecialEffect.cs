namespace Derby.Engine.Models.Cards.Gallop.Effects;

// "I opløbet: Flyt i mål, hvis hesten ikke fører. Bliv ellers stående til næste omgang. Gem kortet til det ikke gælder længere."
public class RepositionSpecialEffect : IGallopCardEffect
{
    public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        var leader = state.GetLeaderHorse();
        if (horseToPlay == leader)
        {
            var skipEffect = new SkipTurnEffect();
            return skipEffect.Resolve(horseToPlay, state);
        }
        else
        {
            var moveEffect = new MoveEffect(99);
            return moveEffect.Resolve(horseToPlay, state);
        }

    }
}