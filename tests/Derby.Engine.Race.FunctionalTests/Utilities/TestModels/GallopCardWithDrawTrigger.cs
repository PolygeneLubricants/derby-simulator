using Derby.Engine.Race.Cards.Gallop;

namespace Derby.Engine.Race.FunctionalTests.Utilities.TestModels;

public class GallopCardWithDrawTrigger : GallopCard
{
    public override GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        OnDraw.Invoke();
        return base.Resolve(horseToPlay, state);
    }

    public event Action OnDraw = () => { };
}