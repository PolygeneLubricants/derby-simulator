using System.CommandLine;

namespace Derby.Simulator.Cmd;

public class RandomRaceCommand
{
    public static Command Create()
    {
        var randomHorseCountOption = new Option<int>(
            name: "--c",
            description: "Number of horses to race. The horses will be equally distributed amongst the players 1 to 5.")
        {
            IsRequired = true
        };


        var randomCommand = new Command("random", "Run a Derby race, where horses are chosen at random, and show the log for each movement.")
        {
            randomHorseCountOption
        };

        randomCommand.SetHandler(randomHorseCount =>
        {
            var simulation = new Simulation();
            simulation.RunRandom(randomHorseCount);
            
        }, randomHorseCountOption);

        return randomCommand;
    }
}