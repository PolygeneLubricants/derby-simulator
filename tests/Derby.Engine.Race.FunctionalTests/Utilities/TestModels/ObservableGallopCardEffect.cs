using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Cards.Gallop.Effects;

namespace Derby.Engine.Race.FunctionalTests.Utilities.TestModels;

public class ObservableGallopCardEffect : IGallopCardEffect
{
    public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        OnResolve.Invoke();
        return new GallopCardResolution();
    }

    public event Action OnResolve = () => { };
}