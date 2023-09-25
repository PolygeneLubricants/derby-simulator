using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.FunctionalTests.Utilities.TestModels;

public class ObservableGallopCard : GallopCard
{
    public override GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        OnDraw.Invoke();
        return base.Resolve(horseToPlay, state);
    }

    public event Action OnDraw = () => { };
}