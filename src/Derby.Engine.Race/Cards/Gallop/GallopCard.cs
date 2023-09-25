using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop;

public class GallopCard : BaseCard<GallopCardResolution, IGallopCardEffect>
{
    public override GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        return CardEffect.Resolve(horseToPlay, state);
    }
}