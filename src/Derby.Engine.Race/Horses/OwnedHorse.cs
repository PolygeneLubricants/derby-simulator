namespace Derby.Engine.Race.Horses;

/// <summary>
///     A horse associated with an owner.
/// </summary>
public class OwnedHorse
{
    /// <summary>
    ///     The owner of this horse.
    /// </summary>
    public Player? Owner { get; init; }

    /// <summary>
    ///     The horse that this owner owns.
    /// </summary>
    public required Horse Horse { get; init; }
}