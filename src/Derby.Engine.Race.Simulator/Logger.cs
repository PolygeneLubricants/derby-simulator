using Derby.Engine.Race.Cards.Chance;
using Derby.Engine.Race.Cards.Gallop;
using Derby.Engine.Race.Turns.Resolutions;

namespace Derby.Engine.Race.Simulator;

public class Logger
{
    public Logger(Race race)
    {
        race.State.GallopDeck.CardDrawn += OnGallopCardDrawn;
        race.State.ChanceDeck.CardDrawn += OnChanceCardDrawn;
        race.TurnResolver.HorseMoved += OnMove;
        race.TurnStarted += OnTurnStarted;
    }
    
    public void Log(ITurnResolution resolution)
    {
        switch (resolution)
        {
            case DrawTurnResolution _:
                Console.WriteLine("Game ended in draw! All horses eliminated.");
                break;
            case EndTurnTurnResolution _:
                break;
            case HorseEliminatedTurnResolution _: 
                Console.WriteLine("Horse eliminated from race.");
                break;
            case HorseWonTurnResolution horseWonResolution:
                Console.WriteLine($"Game ended with winner! Final score:");
                foreach (var scoreString in LogScore(horseWonResolution))
                {
                    Console.WriteLine(scoreString);
                }
                break;
            default:
                throw new Exception($"Unknown resolution type: {resolution.GetType()}");
        }
    }

    private IEnumerable<string> LogScore(HorseWonTurnResolution horseWonResolution)
    {
        return horseWonResolution
            .Score
            .Select((horse, index) => $"{index + 1}: {GetCallerString(horse)}, fields from goal: {horse.FieldsFromGoal}");
    }

    private void OnChanceCardDrawn(HorseInRace horseInRace, ChanceCard card)
    {
        Console.WriteLine($"{GetCallerString(horseInRace)} Chance card drawn: {card.Title}: {card.Description}.");
    }

    private void OnGallopCardDrawn(HorseInRace horseInRace, GallopCard card)
    {
        Console.WriteLine($"{GetCallerString(horseInRace)} Gallop card drawn: {card.Title}: {card.Description}.");
    }

    private void OnMove(HorseInRace horseInRace, int moves)
    {
        Console.WriteLine($"{GetCallerString(horseInRace)} Moved {moves}.");
    }

    private void OnTurnStarted(HorseInRace horseInRace)
    {
        Console.WriteLine($"{GetCallerString(horseInRace)} Turn started.");
    }

    private string GetCallerString(HorseInRace horse)
    {
        return $"[{horse.OwnedHorse.Owner.Stable.Code} - {horse.OwnedHorse.Horse.Name}]";
    }
}