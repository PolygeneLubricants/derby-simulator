using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Chance.Effects;

namespace Derby.Engine.Race.Ruleset.Variations.Derby2023;

/// <summary>
///     The Chance deck used in the 2023 Derby ruleset.
/// </summary>
internal static class Derby2023ChanceDeck
{
    public static ChanceDeck Deck => new(Cards);

    private static IList<ChanceCard> Cards => new List<ChanceCard>
    {
        new()
        {
            Title       = "Banen skal repareres",
            Description = "Alle spillere skal betale et beløb til banen. Under løb 1-6: 2.000 kr. Under løb 7-10: 10.000 kr.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Banen skal bygges om",
            Description = "Alle spillere skal betale halvdelen af deres kontanter til banken.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Dårligt regnskab",
            Description = "Dine selskaber lider. Betal 10.000 kr, til banken for hvert selskab, du ejer.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Ekstra-afgifter til Grand Prix og Derby",
            Description = "Hver spiller skal betale et beløb til banen. Under løb 1-5: Betal 1.000 kr, pr. 3-åring spilleren ejer. Under løb 6-7: Betal 2.000 kr, pr. 4-åring spilleren ejer.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Enestående chance!",
            Description = "Du må købe en hest til halv pris, hvis du har kontanter til det.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Fine regnskaber",
            Description = "Dine selskaber nyder store overskud. Modtag 10.000 kr, fra banken for hvert selskab du ejer.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Forkølelse i stalden",
            Description = "Alle dine heste, som ikke er på banen, er forkølede og kan ikke starte i næste løb. De må heller ikke sælges, før næste løb er startet. Gem kortet indtil da. Hvis du har betalt forsikring, får du 2.000 kr af banken for hver syg hest.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Formueskat",
            Description = "Gælder kun i løb 9 og 10. Du skal betale 20.000 kr til banken.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Forsikringsafgifter",
            Description = "Alle spillere betaler 2.000 kr pr. hest de ejer, til A/S Galopforsikringen. Ejer ingen spillere selskabet, betales der til banken.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Gør en forretning",
            Description = "Du må foretage en valgfri handel, tage et lån eller betale lån tilbage.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Gør en forretning",
            Description = "Du må foretage en valgfri handel, tage et lån eller betale lån tilbage.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Gør en forretning",
            Description = "Du må foretage en valgfri handel, tage et lån eller betale lån tilbage.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Køb en hest",
            Description = "Køb en hest, hvis ud har kontanter til det.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Lotterigevinst",
            Description = "Modtag en valgfri 3-åring. Hvis alle 3-åringer er solgt, modtag 3.000 kr fra banken.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Ombygning af banen billigere end beregnet",
            Description = "Alle spillere modtager fra banken et beløb svarende til hvor mange kontanter de har, dog ikke mere end 25.000 kr.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Overskud fra følauktionen",
            Description = "Modtag 5.000 kr fra banken.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Sælg en hest til banken",
            Description = "Du må sælge en af dine heste til banken, til hestens fulde værdi. Heste der er syge eller deltager i løbet må ikke sælges.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Sælg en hest til A/S Hesteagenturet",
            Description = "A/S Hesteagenturet køber den af dine heste, som er dyrest, og som ikke er med i igangværende løb, for 10.000 kr. Syge heste kan ikke sælges.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Staldleje",
            Description = "Alle spillere skal betale 2.000 kr pr. hest de ejer til A/S GAlopstalden. Hvis en spiller ikke har kontanter nok, er de tvunget til at tage et lån i banken. Hvis A/S Galopstalden ikke ejes af en spiller, betales der til banken.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Staldtips",
            Description = "Du må spille 1.000 kr på en blå hest, hvis der er en sådan med i løbet, og du har kontanter. Gem kortet til løbet er slut.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Staldtips",
            Description = "Du må spille 1.000 kr på en sort hest, hvis der er en sådan med i løbet, og du har kontanter. Gem kortet til løbet er slut.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Staldtips",
            Description = "Du må spille 1.000 kr på en hvid hest, hvis der er en sådan med i løbet, og du har kontanter. Gem kortet til løbet er slut.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Staldtips",
            Description = "Du må spille 1.000 kr på en gul hest, hvis der er en sådan med i løbet, og du har kontanter. Gem kortet til løbet er slut.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Staldtips",
            Description = "Du må spille 1.000 kr på en rød hest, hvis der er en sådan med i løbet, og du har kontanter. Gem kortet til løbet er slut.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Tag et lån",
            Description = "Medmindre du skylder 80.000 kr eller mere, må du tage et lån i banken.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Totosyndikatet giver udbetaling",
            Description = "Totosyndikatets aktionærer modtager en kontant udbetaling fra syndikatets beholdning, svarende til deres andel. Hvis totosyndikatets totale beholdning er mindre end 10.000 kr, sker der ikke noget.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Totosyndikatet giver udbetaling",
            Description = "Totosyndikatets aktionærer modtager en kontant udbetaling fra syndikatets beholdning, svarende til deres andel. Hvis totosyndikatets totale beholdning er mindre end 10.000 kr, sker der ikke noget.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Trænerafgifter",
            Description = "Alle spillere skal betale 3.000 kr pr. hest de ejer til A/S Trænertjenesten. Hvis A/S Trænertjenesten ikke ejes af en spiller, betales der til banken.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Vil du sælge?",
            Description = "Du må sælge et af dine kort, dog ikke syge heste eller heste, der deltager i løbet.",
            CardEffect  = new NoEffect()
        },
        new()
        {
            Title       = "Vil du sælge?",
            Description = "Du må sælge et af dine kort, dog ikke syge heste eller heste, der deltager i løbet.",
            CardEffect  = new NoEffect()
        },
    };

    /// <summary>
    ///     Get a card by its title.
    /// </summary>
    public static ChanceCard GetCard(string title)
    {
        return Cards.First(c => c.Title == title);
    }
}