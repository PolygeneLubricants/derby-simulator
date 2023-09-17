using Derby.Engine.Models.Board.Lanes.Fields;

namespace Derby.Engine.Models.Turns;

public class TurnResolver
{
    public TurnResolution ResolveTurn(
        HorseInRace horseToPlay,
        RaceState state)
    {
        var moves = horseToPlay.OwnedHorse.Horse.GetMoves(state.CurrentTurnNumber);
        if (horseToPlay.Lane.IsHomeStretch(horseToPlay.OwnedHorse, moves))
        {
            var drawnGallopCard = state.GallopDeck.Draw();
            var gallopCardResolution = drawnGallopCard.Resolve(horseToPlay, state);
            if (gallopCardResolution.NewField is GoalField)
            {
                return TurnResolution.HorseWon;
            }
        }
        
        var fieldHorseLandedOn = horseToPlay.Lane.Move(horseToPlay.OwnedHorse, moves);
        return ResolveTurn(horseToPlay, state, fieldHorseLandedOn);
    }

    private TurnResolution ResolveTurn(
        HorseInRace horseToPlay,
        RaceState state,
        IField fieldHorseLandedOn)
    {
        switch (fieldHorseLandedOn)
        {
            case ChanceField _:
                var drawnChanceCard = state.ChanceDeck.Draw();
                var chanceCardResolution = drawnChanceCard.Resolve(horseToPlay, state);
                if (chanceCardResolution.IsHorseEliminated)
                {
                    return TurnResolution.HorseEliminated;
                }

                return TurnResolution.TurnOver;
            case GallopField _:
                var drawnGallopCard = state.GallopDeck.Draw();
                var gallopCardResolution = drawnGallopCard.Resolve(horseToPlay, state);
                if (gallopCardResolution.NewField != null)
                {
                    return ResolveTurn(horseToPlay, state, gallopCardResolution.NewField);
                }

                return TurnResolution.TurnOver;
            case NeutralField _:
                return TurnResolution.TurnOver;
            case GoalField _:
                return TurnResolution.HorseWon;
            default:
                throw new InvalidTurnStateException();
        }
    }
}