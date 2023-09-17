using Derby.Engine.Models.Cards.Gallop.Effects;

namespace Derby.Engine.Models.Cards.Gallop;

public class GallopDeck : BaseDeck<GallopCard>
{
    public GallopDeck()
    {
        Deck = Populate();
    }

    private IList<GallopCard> Populate()
    {
        return new List<GallopCard>
        {
            new() { Title = "Hesten falder tilbage", Description = "Afvent sidste hest. I opløbet, vent en omgang. Gem kortet til det ikke gælder længere." },
            new() { Title = "Hesten kan ikke følge med", Description = "Vent en omgang. Gælder også i opløbet" },
            new() { Title = "Hesten kan ikke følge med", Description = "Vent en omgang. Gælder også i opløbet" },
            new() { Title = "Hesten kan ikke følge med", Description = "Vent en omgang. Gælder også i opløbet" },
            new() { Title = "Stærk fremrykning", Description = "Stil hesten umiddelbart efter førende hest, hvis hesten fører, bliver den stående. I opløbet: Flyt i mål." },
            new() { Title = "Protest", Description = "(Gælder kun i opløbet) Hesten diskvalificeres og tages ud af løbet. Gælder ikke hest redet af stjernejockey." },
            new() { Title = "Hesten forceres", Description = "Flyt 4 felter ekstra frem.", CardEffect = new MoveEffect(4) },
            new() { Title = "Hesten presses", Description = "Flyt et felt ekstra frem.", CardEffect = new MoveEffect(1) },
            new() { Title = "Stærkt tempo", Description = "Hest, som ikke fører, flyttes på linje med forreste hest. I opløbet: Flyt i mål." },
            new() { Title = "Svag slutspurt", Description = "(Gælder kun i opløbet) I denne omgang flyttes højst to felter." },
            new() { Title = "Omplacering i feltet", Description = "Afvent den nærmeste bagved liggende hest. I opløbet: Flyt i mål, hvis hesten ikke fører. Bliv ellers stående til næste omgang. Gem kortet til det ikke gælder længere." },
            new() { Title = "Hesten går udemærket", Description = "Flyt 5 felter ekstra frem.", CardEffect = new MoveEffect(5) },
            new() { Title = "Fin galop", Description = "Ryk to felter ekstra frem.", CardEffect = new MoveEffect(2) },
            new() { Title = "Fin galop", Description = "Ryk to felter ekstra frem.", CardEffect = new MoveEffect(2) },
            new() { Title = "Fin galop", Description = "Ryk to felter ekstra frem.", CardEffect = new MoveEffect(2) },
            new() { Title = "Fin galop", Description = "Ryk to felter ekstra frem.", CardEffect = new MoveEffect(2) },
            new() { Title = "Fin galop", Description = "Ryk to felter ekstra frem.", CardEffect = new MoveEffect(2) },
            new() { Title = "Styrt", Description = "(Gælder ikke for hest redet af stjernejockey) Lad hesten stå til alle er passeret, eller sidste hest er på linje med Deres. Hvis nogen går i mål regnes Deres hest for at være sidst. Gem kortert til det ikke gælder længere." },
            new() { Title = "Pres ikke hesten for hårdt", Description = "(Gælder kun i opløbet) Stå over en omgang." },
            new() { Title = "Jævn fart", Description = "Flyt fra og med næste gang fire felter i hver omgang uden at tage galopkort. I opløbet, Flyt højst fire felter. Kortet opbevares under hele løbet." }
        };
    }
}