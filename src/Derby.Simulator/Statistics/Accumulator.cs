using Derby.Engine.Race.Horses;
using Derby.Engine.Race.Turns.Resolutions;

namespace Derby.Simulator.Statistics;

public class Accumulator
{
    public Accumulator()
    {
        Total = 0;
        DrawnGames = 0;
        Results = new Dictionary<string, AccumulationResult>();
    }

    public void AddResult(ITurnResolution lastTurn, IEnumerable<HorseInRace> result)
    {
        if (lastTurn is DrawTurnResolution)
        {
            DrawnGames++;
        }
        else
        {
            Total++;
            SetOrCreateResult(result);
        }
    }

    public int Total { get; set; }

    public int DrawnGames { get; set; }

    public IDictionary<string, AccumulationResult> Results { get; set; }

    private void SetOrCreateResult(IEnumerable<HorseInRace> result)
    {
        foreach (var (horse, index) in result.Select((horse, index) => (horse, index)))
        {
            SetOrCreateResult(horse.OwnedHorse.Horse.Name, index, horse.Eliminated, horse.GallopCardsDrawn, horse.ChanceCardsDrawn);
        }
    }

    private void SetOrCreateResult(
        string horseName, 
        int place, 
        bool eliminated,
        int gallopCardsDrawn,
        int chanceCardsDrawn)
    {
        if (!Results.ContainsKey(horseName))
        {
            Results.Add(horseName, new AccumulationResult());
        }
        
        if (eliminated)
        {
            Results[horseName].Eliminations++;
        }
        else
        {
            switch (place)
            {
                case 0:
                    Results[horseName].FirstPlaces++; break;
                case 1:
                    Results[horseName].SecondPlaces++; break;
                case 2:
                    Results[horseName].ThirdPlaces++; break;
                case 3:
                    Results[horseName].FourthPlaces++; break;
                case 4:
                    Results[horseName].FifthPlaces++; break;
                default:
                    Results[horseName].BelowFifthPlaces++; break;
            }
        }

        Results[horseName].GallopCardsDrawn += gallopCardsDrawn;
        Results[horseName].ChanceCardsDrawn += chanceCardsDrawn;
    }
}

public class AccumulationResult
{
    public int FirstPlaces { get; set; }
    public int SecondPlaces { get; set; }
    public int ThirdPlaces { get; set; }
    public int FourthPlaces { get; set; }
    public int FifthPlaces { get; set; }
    public int BelowFifthPlaces { get; set; }
    public int Eliminations { get; set; }
    public int GallopCardsDrawn { get; set; }
    public int ChanceCardsDrawn { get; set; }
}