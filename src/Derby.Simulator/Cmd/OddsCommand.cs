using Derby.Engine.Race.Ruleset;
using System.CommandLine;

namespace Derby.Simulator.Cmd;

public class OddsCommand
{
    public static Command Create()
    {
        var horsesOption = new Option<string[]?>(
            name: "--horses",
            description: "Names of the horses in the race. Indicate horses' names. E.g. --horses Avalon Isolde Whispering")
        {
            AllowMultipleArgumentsPerToken = true,
            IsRequired = true
        };

        var heatsOption = new Option<int>(
            name: "--i",
            description: "Number of iterations to run to calculate odds. Defaults to 1.000.",
            getDefaultValue: () => 1000)
        {
            IsRequired = false
        };

        var rulesetOption = new Option<RulesetType>(
            name: "--r",
            description: "Specify the ruleset/version of the board-game to run the simulation against.")
        {
            IsRequired = true
        };

        var oddsCommand = new Command("odds", "Calculates the odds of the specified group of horses running i races.")
        {
            horsesOption, heatsOption, rulesetOption
        };

        oddsCommand.SetHandler((horses, heats, ruleset) =>
        {
            var simulation = new Simulation();
            simulation.RunCombinations(horses, heats, ruleset);

        }, horsesOption, heatsOption, rulesetOption);

        return oddsCommand;
    }
}