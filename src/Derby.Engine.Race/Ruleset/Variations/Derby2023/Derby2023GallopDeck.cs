using Derby.Engine.Race.Cards.Gallop.Effects;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

namespace Derby.Engine.Race.Ruleset.Variations.Derby2023;

/// <summary>
///     The Gallop deck used in the 2023 Derby ruleset.
/// </summary>
internal static class Derby2023GallopDeck
{
    public static GallopCard GetCard(string title)
    {
        return Deck.First(c => c.Title == title);
    }

    public static IList<GallopCard> Deck => new List<GallopCard>
    {
        new() { Title = "Fin galop",            Description = "Ryk to felter ekstra frem.",   CardEffect = new MoveEffect(2) },
        new() { Title = "Fin galop",            Description = "Ryk to felter ekstra frem.",   CardEffect = new MoveEffect(2) },
        new() { Title = "Fin galop",            Description = "Ryk to felter ekstra frem.",   CardEffect = new MoveEffect(2) },
        new() { Title = "Fin galop",            Description = "Ryk to felter ekstra frem.",   CardEffect = new MoveEffect(2) },
        new() { Title = "Fin galop",            Description = "Ryk to felter ekstra frem.",   CardEffect = new MoveEffect(2) },
        new() { Title = "Godt redet!",          Description = "Ryk fire felter ekstra frem.", CardEffect = new MoveEffect(4) },
        new() { Title = "Hesten går udemærket", Description = "Ryk fem felter ekstra frem.",  CardEffect = new MoveEffect(5) },
        new() { 
            Title = "Svag slutspurt",       
            Description = "(Gælder kun i opløbet) I denne omgang flyttes højst to felter.",
            CardEffect = new HomeStretchCompositeEffect(new ModifierEffect(() => new MaxMoveModifier(2)), new NoEffect())
        },
        new() { 
            Title = "Jævn fart",
            Description = "Fra og med næste omgang rykker hesten fire felter frem hver omgang og trækker ikke galopkort, hvis den lander på blå felter. I opløbet: ryk antal felter jf. hestens program, men maksimalt 4 felter. Gem kortet til løbet er slut.",
            CardEffect = new HomeStretchCompositeEffect(new ModifierEffect(() => new MaxMoveModifier(4)), new ModifierEffect(() => new SteadyPaceModifier(4)))
        },
        new()
        {
            Title = "Stærkt tempo!",
            Description = "Hvis hesten ikke fører, rykker den samme antal felter, som den senest har rykket. I opløbet: Flyt i mål.",
            CardEffect =
                new HomeStretchCompositeEffect(new MoveEffect(99), new ConditionalEffect(Condition.IsNotLeader, new MoveRepeaterEffect()))
        },
        new()
        {
            Title = "Pres ikke hesten for hårdt", Description = "(Gælder kun i opløbet) Vent en omgang.",
            CardEffect = new HomeStretchCompositeEffect(new EndTurnAndSkipEffect(), new NoEffect())
        },
        new()
        {
            Title = "Hesten kan ikke følge med", Description = "Vent en omgang.",
            CardEffect = new EndTurnAndSkipEffect()
        },
        new()
        {
            Title = "Hesten kan ikke følge med", Description = "Vent en omgang.",
            CardEffect = new EndTurnAndSkipEffect()
        },
        new()
        {
            Title = "Hesten kan ikke følge med", Description = "Vent en omgang.",
            CardEffect = new EndTurnAndSkipEffect()
        },
        new()
        {
            Title = "Styrt!",
            Description =
                "Lad hesten stå, indtil alle andre heste har passeret eller står på linje med hesten. Rider en anden hest i mål, placerer hesten sidst. Du må ignorere dette kort, hvis hesten rides af en stjernejockey. Gem kortet til det ikke længere er gældende.",
            CardEffect = new CrashSpecialEffect()
        },
        new()
        {
            Title = "Stærk fremrykning!",
            Description =
                "Hvis hesten ikke fører, rykker den frem til feltet bag den førende hest. I opløbet: flyt i mål.",
            CardEffect =
                new HomeStretchCompositeEffect(new MoveEffect(99), new MoveToLeaderEffect(Position.Behind))
        },
        new()
        {
            Title = "Hesten presses!", Description = "Din jockey råber og skriver og presser hesten voldsomt. Ryk ét ekstra felt frem.", CardEffect = new MoveEffect(1)
        },
        new()
        {
            Title = "Protest!",
            Description =
                "(Gælder kun i opløbet) Hesten diskvalificeres og tages ud af løbet. Gælder ikke hvis hesten rides af en stjernejockey.",
            CardEffect = new HomeStretchCompositeEffect(new DisqualifyEffect(), new NoEffect())
        },
        new()
        {
            Title = "Omplacering i feltet",
            Description =
                "Afvent den nærmeste bagvedliggende hest. I opløbet: Flyt i nål, hvis hesten ikke fører. Hvis hesten fører, bliver den stående indtil næste omgang. Gem kortet til det ikke gælder længere.",
            CardEffect = new HomeStretchCompositeEffect(new RepositionSpecialEffect(),
                new ModifierEffect(() => new AwaitModifier(AwaitType.Nearest)))
        },
        new()
        {
            Title = "Hesten er halt!",
            Description =
                "Hesten tages ud af løbet og gives tilbage til banken. Hvis du har en forsikring, modtager du 10.000 kr, af banken.",
            CardEffect = new DisqualifyEffect()
        },
    };
}