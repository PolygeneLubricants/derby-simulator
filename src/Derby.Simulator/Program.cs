using System.CommandLine;
using Derby.Simulator.Cmd;

var rootCommand     = new RootCommand("Run the derby simulator, to play through one or multiple Derby games");
var singleCommand   = SingleRaceCommand.Create();
var randomCommand   = RandomRaceCommand.Create();
var multipleCommand = ManyRaceCommand.Create();
var oddsCommand = OddsCommand.Create();

rootCommand.AddCommand(singleCommand);
rootCommand.AddCommand(randomCommand);
rootCommand.AddCommand(multipleCommand);
rootCommand.AddCommand(oddsCommand);

rootCommand.Invoke(args);