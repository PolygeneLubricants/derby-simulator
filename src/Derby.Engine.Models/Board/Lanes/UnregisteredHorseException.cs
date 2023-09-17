namespace Derby.Engine.Models.Board.Lanes;

public class UnregisteredHorseException : ArgumentException
{
    public UnregisteredHorseException(OwnedHorse ownedHorse) : base($"Player: '{ownedHorse.Owner.Stable.Code}', Horse: '{ownedHorse.Horse.Name}'")
    {
    }
}