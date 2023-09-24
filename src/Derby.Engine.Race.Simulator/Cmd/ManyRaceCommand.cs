using System.CommandLine;

namespace Derby.Engine.Race.Simulator.Cmd;

public class ManyRaceCommand
{
    public static Command Create()
    {
        var combinationMode = new Option<CombinationMode?>(
            name: "--mode",
            description: "Simulation mode to run many games.");

        var sizeOption = new Option<int?>(
            name: "--size",
            description: "Race size. Number of horses in each race. From 1 to 5.");

        var iterationsOption = new Option<int?>(
            name: "--i",
            description: "Number of iterations (times) to run the race for the specified combination.",
            getDefaultValue: () => 1);

        var manyCommand = new Command("many", "Run many Derby races and collect results for all games.")
        {
            combinationMode, sizeOption, iterationsOption
        };

        manyCommand.SetHandler((mode, size, iterations) =>
        {
            if (mode == null)
            {
                throw new ArgumentException("Mode is required.");
            }

            if (size == null)
            {
                throw new ArgumentException("Size is required.");
            }

            if (iterations == null)
            {
                throw new ArgumentException("Iterations is required.");
            }
            
            var simulation = new Simulation();
            simulation.RunCombinations(mode.Value, size.Value, iterations.Value);
        }, combinationMode, sizeOption, iterationsOption);

        return manyCommand;
    }
}