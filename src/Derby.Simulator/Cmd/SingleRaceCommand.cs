using System.CommandLine;

namespace Derby.Simulator.Cmd;

public class SingleRaceCommand
{
    public static Command Create()
    {
        var p1StableOptions = new Option<string[]?>(
            name: "--p1",
            description: "The stable of the first player. Indicate horse name. E.g. --p1 Avalon Isolde")
        {
            AllowMultipleArgumentsPerToken = true
        };

        var p2StableOptions = new Option<string[]?>(
            name: "--p2",
            description: "The stable of the second player. Indicate horse name. E.g. --p2 Avalon Isolde")
        {
            AllowMultipleArgumentsPerToken = true
        };

        var p3StableOptions = new Option<string[]?>(
            name: "--p3",
            description: "The stable of the third player. Indicate horse name. E.g. --p3 Avalon Isolde")
        {
            AllowMultipleArgumentsPerToken = true
        };

        var p4StableOptions = new Option<string[]?>(
            name: "--p4",
            description: "The stable of the fourth player. Indicate horse name. E.g. --p4 Avalon Isolde")
        {
            AllowMultipleArgumentsPerToken = true
        };

        var p5StableOptions = new Option<string[]?>(
            name: "--p5",
            description: "The stable of the fifth player. Indicate horse name. E.g. --p5 Avalon Isolde")
        {
            AllowMultipleArgumentsPerToken = true
        };

        var singleCommand = new Command("single", "Run a single Derby race and show the log for each movement.")
        {
            p1StableOptions, p2StableOptions, p3StableOptions, p4StableOptions, p5StableOptions
        };

        singleCommand.SetHandler((p1, p2, p3, p4, p5) =>
        {
            var simulation = new Simulation();
            simulation.Run(p1, p2, p3, p4, p5);
        }, p1StableOptions, p2StableOptions, p3StableOptions, p4StableOptions, p5StableOptions);

        return singleCommand;
    }
}