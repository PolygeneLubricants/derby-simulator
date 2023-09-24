using System.CommandLine;
using Derby.Engine.Race.Simulator.Cmd;

var rootCommand     = new RootCommand("Run the derby simulator, to play through one or multiple Derby games");
var singleCommand   = SingleRaceCommand.Create();
var multipleCommand = ManyRaceCommand.Create();

rootCommand.AddCommand(singleCommand);
rootCommand.AddCommand(multipleCommand);

rootCommand.Invoke(args);