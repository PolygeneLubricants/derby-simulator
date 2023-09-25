using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop.Effects;

/// <summary>
///     Special card effect that implements the reposition-effect part of the card with the following text:
///     Afvent den nærmeste bagved liggende hest. I opløbet: Flyt i mål, hvis hesten ikke fører. Bliv ellers stående til
///     næste omgang. Gem kortet til det ikke gælder længere.
///     The full card effect is implemented as such:
///     new HomeStretchCompositeEffect(
///       new RepositionSpecialEffect(),
///       new ModifierEffect(() => new AwaitModifier(AwaitType.Nearest)))
/// </summary>
public class RepositionSpecialEffect : IGallopCardEffect
{
    public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        var leader = state.GetLeaderHorse();
        if (horseToPlay == leader)
        {
            var skipEffect = new EndTurnAndSkipEffect();
            return skipEffect.Resolve(horseToPlay, state);
        }

        var moveEffect = new MoveEffect(99);
        return moveEffect.Resolve(horseToPlay, state);
    }
}