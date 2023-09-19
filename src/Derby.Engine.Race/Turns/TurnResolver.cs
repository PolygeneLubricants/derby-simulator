using Derby.Engine.Race.Board.Lanes.Fields;
using Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;
using Derby.Engine.Race.Turns.Resolutions;

namespace Derby.Engine.Race.Turns;

public class TurnResolver
{
    public ITurnResolution ResolveTurn(
        HorseInRace horseToPlay,
        RaceState state)
    {
        // Check pre-move modifiers.
        var modifierResolution = CheckModifiers(horseToPlay, state);
        if (modifierResolution.Any(resolution => resolution.EndTurn))
        {
            return new EndTurnTurnResolution();
        }

        // Check move modifiers
        var moves = horseToPlay.OwnedHorse.Horse.GetMoves(state.CurrentTurn, modifierResolution);
        if (horseToPlay.NextTurnIsHomeStretch(moves) && !ShouldSkipGallopCard(modifierResolution))
        {
            var drawnGallopCard = state.GallopDeck.Draw();
            var gallopCardResolution = drawnGallopCard.Resolve(horseToPlay, state);
            if (gallopCardResolution.HorseDisqualified)
            {
                return new HorseEliminatedTurnResolution(horseToPlay);
            }

            if (gallopCardResolution.NewField is GoalField)
            {
                return new HorseWonTurnResolution(state.GetScore());
            }

            if (gallopCardResolution.EndTurn)
            {
                return new EndTurnTurnResolution();
            }
        }

        // Re-check modifiers based on home-stretch.
        modifierResolution = CheckModifiers(horseToPlay, state);
        if (modifierResolution.Any(resolution => resolution.EndTurn))
        {
            return new EndTurnTurnResolution();
        }

        moves = horseToPlay.OwnedHorse.Horse.GetMoves(state.CurrentTurn, modifierResolution);
        var fieldHorseLandedOn = horseToPlay.Move(moves);
        var turnResolution = ResolveTurn(horseToPlay, state, fieldHorseLandedOn);

        return turnResolution;
    }

    private bool ShouldSkipGallopCard(IEnumerable<ModifierResolution> modifiers)
    {
        return modifiers.Any(modifier => modifier.IsApplicable && modifier.SkipGallopCards);
    }

    private IList<ModifierResolution> CheckModifiers(HorseInRace horseToPlay, RaceState state)
    {
        var applicableModifiers = new List<IModifier>();
        var resolutions = new List<ModifierResolution>();
        foreach (var modifier in horseToPlay.Modifiers)
        {
            var resolution = modifier.Apply(horseToPlay, state);
            if (resolution.IsApplicable)
            {
                applicableModifiers.Add(modifier);
                resolutions.Add(resolution);
            }
        }

        horseToPlay.Modifiers = applicableModifiers;
        return resolutions;
    }

    private ITurnResolution ResolveTurn(
        HorseInRace horseToPlay,
        RaceState state,
        IField fieldHorseLandedOn)
    {
        // Re-check modifiers due to recursive call.
        var modifierResolutions = CheckModifiers(horseToPlay, state);
        switch (fieldHorseLandedOn)
        {
            case ChanceField _:
                var drawnChanceCard = state.ChanceDeck.Draw();
                var chanceCardResolution = drawnChanceCard.Resolve(horseToPlay, state);
                if (chanceCardResolution.IsHorseEliminated)
                {
                    return new HorseEliminatedTurnResolution(horseToPlay);
                }

                return new EndTurnTurnResolution();
            case GallopField _:
                if (ShouldSkipGallopCard(modifierResolutions))
                {
                    return new EndTurnTurnResolution();
                }

                var drawnGallopCard = state.GallopDeck.Draw();
                var gallopCardResolution = drawnGallopCard.Resolve(horseToPlay, state);
                if (gallopCardResolution.HorseDisqualified)
                {
                    return new HorseEliminatedTurnResolution(horseToPlay);
                }
                if (gallopCardResolution.NewField != null)
                {
                    return ResolveTurn(horseToPlay, state, gallopCardResolution.NewField);
                }

                if (gallopCardResolution.EndTurn)
                {
                    return new EndTurnTurnResolution();
                }

                return new EndTurnTurnResolution();
            case NeutralField _:
                return new EndTurnTurnResolution();
            case GoalField _:
                return new HorseWonTurnResolution(state.GetScore());
            default:
                throw new InvalidTurnStateException(fieldHorseLandedOn);
        }
    }
}