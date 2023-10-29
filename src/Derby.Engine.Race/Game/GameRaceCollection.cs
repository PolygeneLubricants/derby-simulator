using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.Game.GlobalModifiers;

namespace Derby.Engine.Race.Game;

/// <summary>
///     Collection of game races (from 1 to 10) in the game.
/// </summary>
public static class GameRaceCollection
{
    public static IList<GameRace> GameRaces => new List<GameRace>
    {
        new("1. løb", "Kun for 2-åringer.",                                new NoGlobalModifier(), new HorsesCriteria(h => h.Years == 2)),
        new("2. løb", "Forårsløb. For alle heste.",                        new NoGlobalModifier(), new HorsesCriteria(_ => true)),
        new("3. løb", "For heste, som ikke tidligere har vundet.",         new NoGlobalModifier(), new HorsesCriteria(_ => true)),
        new("4. løb", "For 3-åringer og ældre heste",                      new GlobalModifier(     new HorsesCriteria(h => h.Years != 3), new EndTurnAndSkipEffect()), new HorsesCriteria(h => h.Years >= 3)),
        new("5. løb", "For heste som ikke tidligere har vundet.",          new NoGlobalModifier(), new HorsesCriteria(_ => true)),
        new("6. løb", "Derby trial stakes. Kun for 3-åringer.",            new NoGlobalModifier(), new HorsesCriteria(h => h.Years == 3)),
        new("7. løb", "For alle heste.",                                   new NoGlobalModifier(), new HorsesCriteria(_ => true)),
        new("8. løb", "4-åringernes Grand Prix. Kun for 4-åringer.",       new NoGlobalModifier(), new HorsesCriteria(h => h.Years == 4)),
        new("9. løb", "For heste, som ikke tidligere har vundet.",         new NoGlobalModifier(), new HorsesCriteria(_ => true)),
        new("10. løb derby", "For heste, som har vundet mindst een gang.", new NoGlobalModifier(), new HorsesCriteria(_ => true))
    };

    /// <summary>
    ///     Gets the game race entity for the current race number.
    /// </summary>
    /// <param name="raceNumber">Race number to get the game race for. Note, that races are numbered from 1 to 10 (not 0).</param>
    public static GameRace GetGameRace(int raceNumber)
    {
        return GameRaces[raceNumber - 1];
    }
}