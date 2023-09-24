using System.CommandLine;

namespace Derby.Engine.Race.Simulator.Cmd;

public class SingleRaceCommand
{
    public static Command Create()
    {
        var player1Stable = new Option<string[]?>(
            name: "--p1",
            description: "The stable of the first player. Indicate horse name. E.g. --p1 Avalon --p1 Isolde");

        var player2Stable = new Option<string[]?>(
            name: "--p2",
            description: "The stable of the second player. Indicate horse name. E.g. --p2 Avalon --p1 Isolde");

        var player3Stable = new Option<string[]?>(
            name: "--p3",
            description: "The stable of the third player. Indicate horse name. E.g. --p3 Avalon --p1 Isolde");

        var player4Stable = new Option<string[]?>(
            name: "--p4",
            description: "The stable of the fourth player. Indicate horse name. E.g. --p4 Avalon --p1 Isolde");

        var player5Stable = new Option<string[]?>(
            name: "--p5",
            description: "The stable of the fifth player. Indicate horse name. E.g. --p5 Avalon --p1 Isolde");

        var gameMode = new Option<SingleGameMode?>(
            name: "--mode",
            description: "Mode to run a single race.", 
            getDefaultValue: () => SingleGameMode.Manual);

        var randomHorses = new Option<int?>(
            name: "--c",
            description: "Number of horses to run if game mode is random.");


        var singleCommand = new Command("single", "Run a single Derby race and show the log for each movement.")
        {
            player1Stable, player2Stable, player3Stable, player4Stable, player5Stable, gameMode, randomHorses
        };

        singleCommand.SetHandler((p1, p2, p3, p4, p5, mode, randCount) =>
        {
            var simulation = new Simulation();
            switch (mode)
            {
                case SingleGameMode.Random:
                    if (randCount == null)
                    {
                        throw new ArgumentException("Random mode requires a number of horses to run.");
                    }

                    simulation.RunRandom(randCount.Value);
                    break;
                case SingleGameMode.Manual:
                    simulation.Run(p1, p2, p3, p4, p5);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
            
        }, player1Stable, player2Stable, player3Stable, player4Stable, player5Stable, gameMode, randomHorses);

        return singleCommand;
    }

    public enum SingleGameMode
    {
        Random, 
        Manual
    }
}