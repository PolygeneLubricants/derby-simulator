using Derby.Engine.Race.Cards.Gallop.Effects;

namespace Derby.Engine.Race.Game.GlobalModifiers;

public class NoGlobalModifier : GlobalModifier
{
    public NoGlobalModifier() : base(new HorsesCriteria(_ => false), new NoEffect())
    {
    }
}