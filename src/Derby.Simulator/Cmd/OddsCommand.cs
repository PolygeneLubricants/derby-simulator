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

        var oddsCommand = new Command("odds", "Calculates the odds of the specified group of horses running i races.")
        {
            horsesOption, heatsOption
        };

        oddsCommand.SetHandler((horses, heats) =>
        {
            var simulation = new Simulation();
            simulation.RunCombinations(horses, heats);

        }, horsesOption, heatsOption);

        return oddsCommand;
    }
}