using Derby.Engine.Race.Cards.Gallop.Effects;

namespace Derby.Engine.Race.Cards.Gallop;

public class GallopCard : BaseCard<GallopCardResolution, IGallopCardEffect>
{
    public override GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        return CardEffect.Resolve(horseToPlay, state);
    }
}