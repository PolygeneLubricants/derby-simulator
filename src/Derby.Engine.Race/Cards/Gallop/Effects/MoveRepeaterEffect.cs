using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Cards.Gallop.Effects;

/// <summary>
///     Repeats the horse' last movement instead of its current movement.
/// </summary>
public class MoveRepeaterEffect : IGallopCardEffect
{
    public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        var turnToGetMoves = GetTurnToGetMoves(horseToPlay, state);
        var moves          = horseToPlay.OwnedHorse.Horse.GetMoves(turnToGetMoves);
        return new MoveEffect(moves).Resolve(horseToPlay, state);
    }

    private static int GetTurnToGetMoves(HorseInRace horseToPlay, RaceState state)
    {
        if (horseToPlay.HasMovedThisTurn || state.CurrentTurn == 0)
        {
            return state.CurrentTurn;
        }

        return state.CurrentTurn - 1;
    }
}