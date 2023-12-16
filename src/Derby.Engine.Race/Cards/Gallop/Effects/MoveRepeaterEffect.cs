using Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;
using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop.Effects;

/// <summary>
///     Repeats the horse' last movement instead of its current movement.
/// </summary>
public class MoveRepeaterEffect : IGallopCardEffect
{
    public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        if (horseToPlay.HasMovedThisTurn)
        {
            var moves = horseToPlay.OwnedHorse.Horse.GetMoves(state.CurrentTurn);
        }
    }
}