using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

namespace Derby.Engine.Race.Ruleset.Variations.Drechsler;

/// <summary>
///     The Gallop deck used in the Drechsler ruleset.
/// </summary>
internal static class DrechslerGallopDeck
{
    public static GallopCard GetCard(string title)
    {
        return Cards.First(c => c.Title == title);
    }

    public static GallopDeck Deck => new(Cards);

    private static IList<GallopCard> Cards => new List<GallopCard>
    {
        new()
        {
            Title = "Hesten falder tilbage",
            Description =
                "Afvent sidste hest. I opløbet, vent en omgang. Gem kortet til det ikke gælder længere.",
            CardEffect = new HomeStretchCompositeEffect(new EndTurnAndSkipEffect(),
                new ModifierEffect(() => new AwaitModifier(AwaitType.Last)))
        },
        new()
        {
            Title = "Hesten kan ikke følge med", Description = "Vent en omgang. Gælder også i opløbet",
            CardEffect = new EndTurnAndSkipEffect()
        },
        new()
        {
            Title = "Hesten kan ikke følge med", Description = "Vent en omgang. Gælder også i opløbet",
            CardEffect = new EndTurnAndSkipEffect()
        },
        new()
        {
            Title = "Hesten kan ikke følge med", Description = "Vent en omgang. Gælder også i opløbet",
            CardEffect = new EndTurnAndSkipEffect()
        },
        new()
        {
            Title = "Stærk fremrykning",
            Description =
                "Stil hesten umiddelbart efter førende hest, hvis hesten fører, bliver den stående. I opløbet: Flyt i mål.",
            CardEffect =
                new HomeStretchCompositeEffect(new MoveEffect(99), new MoveToLeaderEffect(Position.Behind))
        },
        new()
        {
            Title = "Protest",
            Description =
                "(Gælder kun i opløbet) Hesten diskvalificeres og tages ud af løbet. Gælder ikke hest redet af stjernejockey.",
            CardEffect = new HomeStretchCompositeEffect(new DisqualifyEffect(), new NoEffect())
        },
        new()
        {
            Title = "Hesten forceres", Description = "Flyt 4 felter ekstra frem.",
            CardEffect = new MoveEffect(4)
        },
        new()
        {
            Title = "Hesten presses", Description = "Flyt et felt ekstra frem.", CardEffect = new MoveEffect(1)
        },
        new()
        {
            Title = "Stærkt tempo",
            Description = "Hest, som ikke fører, flyttes på linje med forreste hest. I opløbet: Flyt i mål.",
            CardEffect =
                new HomeStretchCompositeEffect(new MoveEffect(99), new MoveToLeaderEffect(Position.OnPar))
        },
        new()
        {
            Title = "Svag slutspurt",
            Description = "(Gælder kun i opløbet) I denne omgang flyttes højst to felter.",
            CardEffect = new HomeStretchCompositeEffect(new ModifierEffect(() => new MaxMoveModifier(2)),
                new NoEffect())
        },
        new()
        {
            Title = "Omplacering i feltet",
            Description =
                "Afvent den nærmeste bagved liggende hest. I opløbet: Flyt i mål, hvis hesten ikke fører. Bliv ellers stående til næste omgang. Gem kortet til det ikke gælder længere.",
            CardEffect = new HomeStretchCompositeEffect(new RepositionSpecialEffect(),
                new ModifierEffect(() => new AwaitModifier(AwaitType.Nearest)))
        },
        new()
        {
            Title = "Hesten går udemærket", Description = "Flyt 5 felter ekstra frem.",
            CardEffect = new MoveEffect(5)
        },
        new()
        {
            Title = "Fin galop", Description = "Ryk to felter ekstra frem.", CardEffect = new MoveEffect(2)
        },
        new()
        {
            Title = "Fin galop", Description = "Ryk to felter ekstra frem.", CardEffect = new MoveEffect(2)
        },
        new()
        {
            Title = "Fin galop", Description = "Ryk to felter ekstra frem.", CardEffect = new MoveEffect(2)
        },
        new()
        {
            Title = "Fin galop", Description = "Ryk to felter ekstra frem.", CardEffect = new MoveEffect(2)
        },
        new()
        {
            Title = "Fin galop", Description = "Ryk to felter ekstra frem.", CardEffect = new MoveEffect(2)
        },
        new()
        {
            Title = "Styrt",
            Description =
                "(Gælder ikke for hest redet af stjernejockey) Lad hesten stå til alle er passeret, eller sidste hest er på linje med Deres. Hvis nogen går i mål regnes Deres hest for at være sidst. Gem kortert til det ikke gælder længere.",
            CardEffect = new CrashSpecialEffect()
        },
        new()
        {
            Title = "Pres ikke hesten for hårdt", Description = "(Gælder kun i opløbet) Stå over en omgang.",
            CardEffect = new HomeStretchCompositeEffect(new EndTurnAndSkipEffect(), new NoEffect())
        },
        new()
        {
            Title = "Jævn fart",
            Description =
                "Flyt fra og med næste gang fire felter i hver omgang uden at tage galopkort. I opløbet, Flyt højst fire felter. Kortet opbevares under hele løbet.",
            CardEffect = new HomeStretchCompositeEffect(new ModifierEffect(() => new MaxMoveModifier(4)),
                new ModifierEffect(() => new SteadyPaceModifier(4)))
        }
    };
}