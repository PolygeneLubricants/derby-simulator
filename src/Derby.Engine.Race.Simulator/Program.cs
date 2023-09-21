// See https://aka.ms/new-console-template for more information

using Derby.Engine.Race;
using Derby.Engine.Race.Horses;
using Derby.Engine.Race.Simulator;
using Derby.Engine.Race.Turns.Resolutions;

Console.WriteLine("Hello, World!");


var horsesToRace = new List<OwnedHorse>()
{
    new OwnedHorse { Owner = new Player { Stable = new Stable { Code = StableCode.A } }, Horse = HorseCollection.Get("Caruso") },
    new OwnedHorse { Owner = new Player { Stable = new Stable { Code = StableCode.B } }, Horse = HorseCollection.Get("Rapid") },
    new OwnedHorse { Owner = new Player { Stable = new Stable { Code = StableCode.C } }, Horse = HorseCollection.Get("Solitude") },
    new OwnedHorse { Owner = new Player { Stable = new Stable { Code = StableCode.D } }, Horse = HorseCollection.Get("Rigel") },
    new OwnedHorse { Owner = new Player { Stable = new Stable { Code = StableCode.E } }, Horse = HorseCollection.Get("Pollux") },

};

var race = Race.GetDefault(horsesToRace);
var logger = new Logger(race);

var turnLimit = 100_000;

while (!race.GameEnded && turnLimit > race.State.CurrentTurn)
{
    var resolution = race.ResolveTurn();
    logger.Log(resolution);
}