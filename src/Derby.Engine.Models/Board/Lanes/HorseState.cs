namespace Derby.Engine.Models.Board.Lanes;

internal class HorseState
{
    public required OwnedHorse Horse { get; init; }

    public required int Location { get; set; }
}