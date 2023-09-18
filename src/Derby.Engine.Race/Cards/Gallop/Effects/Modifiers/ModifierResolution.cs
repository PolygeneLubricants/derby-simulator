namespace Derby.Engine.Race.Cards.Gallop.Effects.Modifiers;

public class ModifierResolution
{
    public required bool IsApplicable { get; set; }

    public bool EndTurn { get; set; }

    public int? MaxMoves { get; set; }

    public int? Moves { get; set; }

    public bool SkipGallopCards { get; set; }

    public bool CountAsLast { get; set; }
}