﻿using Derby.Engine.Race.Cards.Chance.Effects;

namespace Derby.Engine.Race.Cards.Chance;

public class ChanceCard : BaseCard<ChanceCardResolution, IChanceCardEffect>
{
    public override ChanceCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        return CardEffect.Resolve(horseToPlay, state);
    }
}