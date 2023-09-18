using Derby.Engine.Models.Cards.Chance.Effects;

namespace Derby.Engine.Models.Cards.Chance;

public class ChanceCard : BaseCard<ChanceCardResolution, IChanceCardEffect>
{
    public override ChanceCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        throw new NotImplementedException();
    }
}