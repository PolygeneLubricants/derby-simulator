using Derby.Engine.Race.Simulator;

var simulation = new Simulation();
// simulation.RunCombinations(CombinationRule.All);

simulation.Run(new List<string> { "Isolde", "Rusch", "Whispering", "Sweet Sue", "Aldebaran" });