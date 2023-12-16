using Derby.Engine.Race.Board.Lanes.Fields;
using Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;
using Derby.Engine.Race.Horses;
using Derby.Engine.Race.Turns.Resolutions;

namespace Derby.Engine.Race.Turns;

/// <summary>
///     Turn resolver is a helper-class, which helps to resolve a turn for a horse.
/// </summary>
public class TurnResolver
{
    /// <summary>
    ///     Event listener when fires when a horse has moved naturally (not through a Gallop card).
    /// </summary>
    public event Action<HorseInRace, int> HorseMoved = (horse, i) => { };

    /// <summary>
    ///     Resolve a turn for a horse.
    /// </summary>
    public ITurnResolution ResolveTurn(
        HorseInRace horseToPlay,
        RaceState state)
    {
        var homeStretchResolution = ResolveHomeStretch(horseToPlay, state);
        if (homeStretchResolution != null)
        {
            return homeStretchResolution;
        }

        // Re-check modifiers based on home-stretch.
        var modifierResolution = CheckModifiers(horseToPlay, state);
        if (modifierResolution.Any(resolution => resolution.EndTurn))
        {
            return new EndTurnTurnResolution();
        }

        var moves              = horseToPlay.OwnedHorse.Horse.GetMoves(state.CurrentTurn, modifierResolution);
        var fieldHorseLandedOn = horseToPlay.Move(moves, MoveType.Natural);
        HorseMoved.Invoke(horseToPlay, moves);

        var turnResolution = ResolveTurn(horseToPlay, state, fieldHorseLandedOn);

        return turnResolution;
    }

    /// <summary>
    ///     Indicates whether the horse should skip a Gallop card.
    ///     E.g. the horse has collected a 'Steady Pace' gallop card, and no longer collects Gallop cards.
    /// </summary>
    private bool ShouldSkipGallopCard(IEnumerable<ModifierResolution> modifiers)
    {
        return modifiers.Any(modifier => modifier.IsApplicable && modifier.SkipGallopCards);
    }

    /// <summary>
    ///     Check if there are any active modifiers on the <see cref="HorseInRace" />.
    ///     If any modifiers are applicable, they are returned. Otherwise they are removed from the list of modifiers for this
    ///     horse.
    /// </summary>
    private IList<ModifierResolution> CheckModifiers(HorseInRace horseToPlay, RaceState state)
    {
        var applicableModifiers = new List<IModifier>();
        var resolutions         = new List<ModifierResolution>();
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

    /// <summary>
    ///     Resolve the home-stretch part of a turn. This happens in the beginning of the turn,
    ///     where a check determines if the horse will reach goal this turn.
    ///     If so, a special set of rules applies.
    /// </summary>
    private ITurnResolution? ResolveHomeStretch(HorseInRace horseToPlay, RaceState state)
    {
        // Check pre-move modifiers.
        var modifierResolution = CheckModifiers(horseToPlay, state);
        if (modifierResolution.Any(resolution => resolution.EndTurn))
        {
            return new EndTurnTurnResolution();
        }

        // Check move modifiers
        if (horseToPlay.TurnIsHomeStretch(state.CurrentTurn))
        {
            var drawnGallopCard      = state.GallopDeck.Draw(horseToPlay);
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

        return null;
    }

    /// <summary>
    ///     Resolve the turn after home stretch has been resolved.
    /// </summary>
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
                var drawnChanceCard      = state.ChanceDeck.Draw(horseToPlay);
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

                var drawnGallopCard      = state.GallopDeck.Draw(horseToPlay);
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