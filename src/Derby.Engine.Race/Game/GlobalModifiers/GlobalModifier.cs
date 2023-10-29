using Derby.Engine.Race.Cards.Gallop.Effects;

namespace Derby.Engine.Race.Game.GlobalModifiers;

public class GlobalModifier : IGlobalModifier
{
    public GlobalModifier(
        HorsesCriteria horsesUnderEffectCriteria, 
        IGallopCardEffect effectAtBeginningOfGame)
    {
        HorsesUnderEffectCriteria = horsesUnderEffectCriteria;
        EffectAtBeginningOfGame = effectAtBeginningOfGame;
    }

    public HorsesCriteria HorsesUnderEffectCriteria { get; init; }

    public IGallopCardEffect EffectAtBeginningOfGame { get; init; }
}