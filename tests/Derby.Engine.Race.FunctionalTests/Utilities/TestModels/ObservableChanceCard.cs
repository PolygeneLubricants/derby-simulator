using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.FunctionalTests.Utilities.TestModels;

public class ObservableChanceCard : ChanceCard
{
    public override ChanceCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        OnDraw.Invoke();
        return base.Resolve(horseToPlay, state);
    }

    public event Action OnDraw = () => { };
}