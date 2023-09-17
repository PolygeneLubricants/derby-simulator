namespace Derby.Engine.Models.Cards;

public class ChanceDeck
{
    public ChanceDeck()
    {
        Deck = Populate();
    }

    public IList<ChanceCard> Deck { get; set; }

    private IList<ChanceCard> Populate()
    {
        return new List<ChanceCard>
        {
            new() { Title = "Staldtips", Description = "Spil 1.000 kr på blå hest, hvis der er en sådan med i løbet og De har kontanter. Gem kortet til løbet er forbi." },
            new() { Title = "Staldtips", Description = "Spil 1.000 kr på sort hest, hvis der er en sådan med i løbet og De har kontanter. Gem kortet til løbet er forbi." },
            new() { Title = "Staldtips", Description = "Spil 1.000 kr på hvid hest, hvis der er en sådan med i løbet og De har kontanter. Gem kortet til løbet er forbi." },
            new() { Title = "Staldtips", Description = "Spil 1.000 kr på gul hest, hvis der er en sådan med i løbet og De har kontanter. Gem kortet til løbet er forbi." },
            new() { Title = "Staldtips", Description = "Spil 1.000 kr på rød hest, hvis der er en sådan med i løbet og De har kontanter. Gem kortet til løbet er forbi." },
            new() { Title = "Vil de sælge?", Description = "Hvad som helst må sælges, dog ikke heste på banen." },
            new() { Title = "Lottogevinst", Description = "De har vundet en 3-åring, eller, hvis alle 3-åringer er solgt, 3.000 kr." },
            new() { Title = "Totosyndikatet giver udbetaling", Description = "(Gælder kun aktieejere) Hvis syndikatets beholdning udgør mindst 10.000 kr, sker udbetalingen ifølge bestemmelserne på aktierne." },
            new() { Title = "Totosyndikatet giver udbetaling", Description = "(Gælder kun aktieejere) Hvis syndikatets beholdning udgør mindst 10.000 kr, sker udbetalingen ifølge bestemmelserne på aktierne." },
            new() { Title = "Gør en forretning", Description = "Foretag en valgfri forretning efter reglerne." },
            new() { Title = "Gør en forretning", Description = "Foretag en valgfri forretning efter reglerne." },
            new() { Title = "Stald-leje", Description = "(Gælder alle) Betal 2.000 kr til A/S Gallopstald for hver hest De ejer. Tag lån hvis De ikke har kontanter. Betal til banken, hvis selskabet ikke er solgt." },
            new() { Title = "Banen må repareres", Description = "(Gælder alle) Under løb 1-6 Hver deltager betaler 2.000 kr. Under løb 7-10 Hver deltager betaler 10.000 kr." },
            new() { Title = "Fint regnskab", Description = "(Gælder ikke øvrige deltagere) Deres selskaber viser stort overskud. Modtag 10.000 kr fra banken for hvert selskab De ejer." },
            new() { Title = "Ombygning af banen billigere end beregnet", Description = "(Gælder alle) Deres kontanter fordobles gennem udbetaling fra banken, dog højst 25.000 kr." },
            new() { Title = "Sælg en hest", Description = "A/S HESTEAGENTURET køber den af Deres heste, som er dyrest og som ikke er med i løbet for 10.000 kr." },
            new() { Title = "Dårligt regnskab", Description = "(Gælder ikke øvrige deltagere) Deres forretninger viser stort underskud - betal 10.000 kr til banken for hvert selskab De ejer." },
            new() { Title = "Sælg en hest", Description = "Banken køber en af Deres heste til fuld værdi. Gælder ikke heste der er med i indeværende løb og ikke heste med startforbud." },
            new() { Title = "Tag et lån", Description = "Gælder ikke hvis De har 60.000 kr eller mere i gæld." },
            new() { Title = "Forkølelse i stalden", Description = "Alle Deres heste som ikke er på banen, er forkølede og kan ikke starte i næste løb. De må heller ikke sælges, før næste løb er startet. Gem kortet så længe." },
            new() { Title = "Vil de købe en hest?", Description = "Gør det nu, hvis De har kontanter." },
            new() { Title = "Enestående tilfælde", Description = "De må købe en hest til halv pris, hvis De har kontanter." },
            new() { Title = "Banen skal bygges om", Description = "(Gælder alle) Betal halvdelen af Deres kontanter til banken." },
            new() { Title = "Formueskat", Description = "(Gælder kun i løb 9 og 10) Betal 20.000 kr til banken. Gælder ikke øvrige deltagere." },
            new() { Title = "Overskud fra følauktionen", Description = "Modtag 5.000 kr fra banken." },
            new() { Title = "Hesten halt", Description = "Tag den ud af løbet og sælg den til indehaveren af A/S Hesteagenturet for 500 kr. Er selskabet ikke solgt, sælges hesten til banken for 500 kr." },
            new() { Title = "Forsikringsafgifter", Description = "(Gælder alle) Betal 2.000 kr til A/S Gallopforsikringen for hver hest De ejer. Tag lån hvis De ikke har konstanter. Er selskabet ikke solgt, betales der til banken." },
            new() { Title = "Køb en hest", Description = "Gælder kun hvis De har kontanter." },
            new() { Title = "Trænerafgifter", Description = "(Gælder alle) Betal til A/S Trænertjenesten 3.000 kr pr. hest, De ejer. Tag lån hvis De ikke har kontanter. Er selskabet ikke solgt, betales til banken." },
            new() { Title = "Ekstra afgifter til Grand Prix og Derby", Description = "(Gælder alle) Under løb 1-5 Betal 1.000 kr pr. 3-åring. Under løb 6-7 Betal 2.000 kr pr. 4-åring." }
        };
    }
}