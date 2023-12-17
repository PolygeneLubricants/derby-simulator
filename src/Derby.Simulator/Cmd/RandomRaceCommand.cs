using Derby.Engine.Race.Ruleset;
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

        var rulesetOption = new Option<RulesetType>(
            name: "--r",
            description: "Specify the ruleset/version of the board-game to run the simulation against.")
        {
            IsRequired = true
        };


        var randomCommand = new Command("random", "Run a Derby race, where horses are chosen at random, and show the log for each movement.")
        {
            randomHorseCountOption, rulesetOption
        };

        randomCommand.SetHandler((randomHorseCount, ruleset) =>
        {
            var simulation = new Simulation();
            simulation.RunRandom(randomHorseCount, ruleset);
            
        }, randomHorseCountOption, rulesetOption);

        return randomCommand;
    }
}